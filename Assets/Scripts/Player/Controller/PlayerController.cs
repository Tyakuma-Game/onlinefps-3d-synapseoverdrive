using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

/// <summary>
/// Player管理クラス
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    UIManager uIManager;        //UI管理
    SpawnManager spawnManager;  //スポーンマネージャー管理
    GameManager gameManager;    //ゲームマネージャー

    //−−−−−−−−−−−−−−−−−−−−−/
    //　効率化中
    //−−−−−−−−−−−−−−−−−−−−−/

    [Tooltip("プレイヤーのステータス情報")]
    [SerializeField] PlayerStatus playerStatus;

    // Player機能
    [Tooltip("Playerの移動処理")]
    IPlayerMove playerMove;

    [Tooltip("Playerの回転処理")]
    IPlayerRotation playerRotation;

    [Tooltip("Playerのジャンプ処理")]
    IPlayerJump playerJump;

    [Tooltip("Playerのアニメーション処理")]
    IPlayerAnimator playerAnimator;

    [Tooltip("着地しているか判定処理")]
    PlayerLandDetector playerLandDetector;

    [SerializeField] PlayerSoundManager playerSoundManager;


    // 入力システム
    [Tooltip("キーボードの入力処理")]
    IKeyBoardInput keyBoardInput;
    
    [Tooltip("マウスの入力処理")]
    IMouseInput mouseInput;

    IMouseCursorLock mouseCursorLock;

    Rigidbody myRigidbody;
    Camera myCamera;

    [SerializeField] CameraController cameraController;


    TestAnimatorController testAnimatorController;
    [SerializeField] GameObject spawnEffect;


    bool isShowDeath = false;

    [PunRPC]
    public void SpawnEffectActive()
    {
        spawnEffect.SetActive(true);
    }

    public void SpawnEffectNotActive()
    {
        spawnEffect.SetActive(false);
    }


    //−−−−−−−−−−−−−−−−−−−−−/

    void Awake()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //タグからUIManagerを探す
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //タグからUIManagerを探す
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        //タグからSpawnManagerを探す
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

    }

    void Start()
    {
        // 指定時間後に演出を停止させる
        Invoke("SpawnEffectNotActive", 1.5f);

        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        myRigidbody = GetComponent<Rigidbody>();
        myCamera = Camera.main;

        // 入力システム
        keyBoardInput = GetComponent<IKeyBoardInput>();
        mouseInput = GetComponent<IMouseInput>();
        mouseCursorLock = GetComponent<IMouseCursorLock>();
        mouseCursorLock.LockScreen();

        // Playerシステム
        playerLandDetector = GetComponent<PlayerLandDetector>();
        playerAnimator = GetComponent<IPlayerAnimator>();
        playerMove = GetComponent<IPlayerMove>();
        playerJump = GetComponent<IPlayerJump>();
        playerRotation = GetComponent<IPlayerRotation>();

        // ステータス初期化
        playerMove.Init(myRigidbody);
        playerJump.Init(myRigidbody);
        playerStatus.Init();

        //HPスライダー反映
        uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);

        testAnimatorController = GetComponent<TestAnimatorController>();
        testAnimatorController.TestSetHP(playerStatus.CurrentHP);
    }


    void Update()
    {
        // 自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        // 死亡演出中なら
        if (isShowDeath)
        {
            Debug.Log("死亡演出で処理を中断させてます。");

            // 処理終了
            return;
        }

        //−−−−−−−−−−−−−−−−−−−−−/
        // 状態変更処理
        //−−−−−−−−−−−−−−−−−−−−−/
        {
            // マウスカーソルのロック状態変更
            if (keyBoardInput.GetCursorLockKeyInput())
            {
                if (mouseCursorLock.IsLocked())
                    mouseCursorLock.LockScreen();
                else
                    mouseCursorLock.UnlockScreen();
            }

            // 状態遷移
            if (keyBoardInput.GetRunKeyInput())
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Run)
                    playerStatus.IsRunning();
            }
            else
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Walk)
                    playerStatus.IsWalking();
            }
        }

        //−−−−−−−−−−−−−−−−−−−−−/
        // PLAYER処理
        //−−−−−−−−−−−−−−−−−−−−−/
        {
            // 回転
            Vector2 roteDirection = mouseInput.GetMouseMove();
            if (roteDirection != Vector2.zero)
            {
                playerRotation.Rotation(roteDirection, playerStatus.Constants.RotationSpeed);
                cameraController.Rotation(roteDirection, playerStatus.Constants.RotationSpeed, playerStatus.Constants.VerticalRotationRange);
            }

            // 移動
            Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
            if (moveDirection != Vector3.zero)
            {
                playerMove.Move(moveDirection, playerStatus.ActiveMoveSpeed);
            }
            else
            {
                playerStatus.IsIdol();
            }

            // ジャンプ
            if (playerLandDetector.IsGrounded)
            {
                if (keyBoardInput.GetJumpKeyInput())
                {
                    playerJump.Jump(playerStatus.ActiveJumpForth);
                    playerLandDetector.OnJunpingChangeFlag();
                }
            }

            //−−−−−−−−−−−−−−−−−−−−−/
            // アニメーション更新
            //−−−−−−−−−−−−−−−−−−−−−/
            testAnimatorController.TestIsGround(playerLandDetector.IsGrounded);
            if (testAnimatorController == null)
            {
                Debug.Log("testAnimatorControllerがNULL!!");
            }
            else
            {
                float moveSpeed = moveDirection.magnitude*playerStatus.ActiveMoveSpeed;
                testAnimatorController.TestMove(moveSpeed);
            }
            

            if (playerLandDetector.IsGrounded == false)
            {
                playerStatus.IsIdol();
            }

            // Sound処理
            playerSoundManager.SoundPlays(playerStatus.AnimationState);
        }

        if (playerStatus.AnimationState == PlayerAnimationState.Run)
        {
            uIManager.IsRunning();
        }
        else
        {
            uIManager.IsNotRunning();
        }

        //−−−−−−−−−−−−−−−−−−−−−/
        // カメラ処理
        //−−−−−−−−−−−−−−−−−−−−−/

        // カメラの座標更新
        cameraController.UpdatePosition();

        // テストコード
        if(Input.GetKeyDown(KeyCode.M))
        {
            // Damageを受けた際の音を鳴らす
            playerSoundManager.DamageSound();

            //ダメージ
            playerStatus.OnDamage(10);

            // アニメーション
            testAnimatorController.TestSetHP(playerStatus.CurrentHP);
            testAnimatorController.Damage();

            //カメラを揺らす
            cameraController.Shake();

            //HPをスライダーに反映
            uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
        }
    }


    /// <summary>
    /// 弾に当たった時呼ばれる処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <param name="name">撃ったやつの名前</param>
    /// <param name="actor">撃ったやつの番号</param>
    [PunRPC]
    public void Hit(int damage, string name, int actor)
    {
        //ダメージ関数呼び出し
        ReceiveDamage(name, damage, actor);
    }


    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <param name="name">撃ったやつの名前</param>
    /// <param name="actor">撃ったやつの番号</param>
    public void ReceiveDamage(string name, int damage, int actor)
    {
        //自分なら
        if (photonView.IsMine)
        {
            // Damageを受けた際の音を鳴らす
            playerSoundManager.DamageSound();

            //ダメージ
            playerStatus.OnDamage(damage);
            
            // アニメーション
            testAnimatorController.TestSetHP(playerStatus.CurrentHP);
            testAnimatorController.Damage();

            //カメラを揺らす
            cameraController.Shake();

            //現在のHPが0以下の場合
            if (playerStatus.CurrentHP <= 0 && isShowDeath == false)
            {
                //死亡関数を呼ぶ
                Death(name, actor);
            }

            //HPをスライダーに反映
            uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
        }
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Death(string name, int actor)
    {
        //死亡UIを更新
        uIManager.UpdateDeathUI(name);

        //自分のデス数を上昇(自分の識別番号、デス、加算数値)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //撃ってきた相手のキル数を上昇(撃ってきた敵の識別番号、キル、加算数値)
        gameManager.ScoreGet(actor, 0, 1);

        // 死亡演出変更
        isShowDeath = true;

        // 消滅パーティクル出現
        photonView.RPC("SpawnEffectActive",RpcTarget.All);

        //死亡関数を呼び出し
        spawnManager.Die();
    }


    /// <summary>
    /// Playerの始末処理
    /// </summary>
    public void OutGame()
    {
        // GameManagerオブジェクトを参照
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //プレイヤーデータ削除
        gameManager.OutPlayerGet(PhotonNetwork.LocalPlayer.ActorNumber);

        //同期を切断
        PhotonNetwork.AutomaticallySyncScene = false;

        //ルームから退出
        PhotonNetwork.LeaveRoom();
    }
}