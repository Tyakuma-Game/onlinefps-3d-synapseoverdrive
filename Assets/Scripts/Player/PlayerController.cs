using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Playables;

public struct PlayerStatus
{
    public float activeMoveSpeed;                       // 現在の移動速度
    public Vector3 jumpForth;                           // ジャンプ力
    public PlayerAnimationState playerAnimationState;   // 現在の状態
}

/// <summary>
/// Player管理クラス
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    [Header("定数")]
    [Tooltip("PlayerのHP最大値")]
    [SerializeField] int PLAYER_MAX_HP = 100;   //最大HP
    int currentHp;                              //現在のHP

    [Header("参照")]

    [Tooltip("Playerの視点移動に関するクラス")]
    [SerializeField] PlayerViewpointShift playerViewpointShift;

    [Tooltip("Playerの銃管理クラス")]
    [SerializeField] PlayerGunController playerGunController;

    UIManager uIManager;        //UI管理
    SpawnManager spawnManager;  //スポーンマネージャー管理
    GameManager gameManager;    //ゲームマネージャー

    //−−−−−−−−−−−−−−−−−−−−−/
    //　効率化中のProgram
    //−−−−−−−−−−−−−−−−−−−−−/

    [Tooltip("プレイヤーのStatus情報")]
    PlayerStatus playerStatus;


    // Player機能
    [Tooltip("Playerの移動処理")]
    IPlayerMove playerMove;

    [Tooltip("Playerのジャンプ処理")]
    IPlayerJump playerJump;

    [Tooltip("Playerのアニメーション処理")]
    IPlayerAnimator playerAnimator;

    [Tooltip("着地しているか判定処理")]
    PlayerLandDetector playerLandDetector;


    // 入力システム
    [Tooltip("キーボードの入力処理")]
    IKeyBoardInput keyBoardInput;


    [SerializeField] float PLAYER_WALK_SPEED = 4f;
    [SerializeField] float PLAYER_RUN_SPEED = 8f;
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0,3f,0);

    //−−−−−−−−−−−−−−−−−−−−−/

    private void Awake()
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
        //現在のHPをMAXHPの数値に設定
        currentHp = PLAYER_MAX_HP;

        //HPをスライダーに反映
        uIManager.UpdateHP(PLAYER_MAX_HP, currentHp);


        // 入力システム
        keyBoardInput = GetComponent<IKeyBoardInput>();

        // Playerシステム
        playerAnimator = GetComponent<PlayerAnimator>();
        playerMove = GetComponent<IPlayerMove>();
        playerJump = GetComponent<IPlayerJump>();
        playerJump = GetComponent<IPlayerJump>();
        playerLandDetector = GetComponent<PlayerLandDetector>();

        // 初期化処理
        playerStatus.activeMoveSpeed = PLAYER_WALK_SPEED;               // 移動速度
        playerStatus.jumpForth = PLAYER_JUMP_FORTH;                     // ジャンプ力
        playerStatus.playerAnimationState = PlayerAnimationState.Idol;  // 状態
    }


    void Update()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //　毎回走っていても歩きになり、ろくな条件式でないから修正する
        //−−−−−−−−−−−−−−−−−−−−−/

        // 走り状態遷移 走りキー入力されている＆走り状態でないなら
        if (keyBoardInput.GetRunKeyInput() && !(playerStatus.playerAnimationState == PlayerAnimationState.Run))
        {
            playerStatus.activeMoveSpeed = PLAYER_RUN_SPEED;
            playerStatus.playerAnimationState = PlayerAnimationState.Run;
        }// 走るキーが押されていない & 状態が歩き状態ではない
        else if (!keyBoardInput.GetRunKeyInput() && !(playerStatus.playerAnimationState == PlayerAnimationState.Walk))
        {
            playerStatus.activeMoveSpeed = PLAYER_WALK_SPEED;
            playerStatus.playerAnimationState = PlayerAnimationState.Walk;
        }

        //−−−−−−−−−−−−−−−−−−−−−/

        // 移動
        Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
        if (moveDirection != Vector3.zero)
        {
            playerMove.Move(moveDirection, playerStatus.activeMoveSpeed);
        }
        else
        {
            playerStatus.playerAnimationState = PlayerAnimationState.Idol;
        }

        // ジャンプ
        if (playerLandDetector.IsGrounded)
        {
            Debug.Log("地面にいます");
            if (keyBoardInput.GetJumpKeyInput())
            {
                playerJump.Jump(playerStatus.jumpForth);
            }
        }
        else
        {
            Debug.Log("地面にいません");
        }

        // アニメーション更新
        playerAnimator.AnimationUpdate(playerStatus.playerAnimationState);
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
            //ダメージ
            currentHp -= damage;

            //現在のHPが0以下の場合
            if (currentHp <= 0)
            {
                //死亡関数を呼ぶ
                Death(ref currentHp, name, actor);
            }

            //HPをスライダーに反映
            uIManager.UpdateHP(PLAYER_MAX_HP, currentHp);
        }
    }


    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Death(ref int currentHp, string name, int actor)
    {
        //現在のHPを０に調整
        currentHp = 0;

        //死亡関数を呼び出し
        spawnManager.Die();

        //死亡UIを更新
        uIManager.UpdateDeathUI(name);

        //自分のデス数を上昇(自分の識別番号、デス、加算数値)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //撃ってきた相手のキル数を上昇(撃ってきた敵の識別番号、キル、加算数値)
        gameManager.ScoreGet(actor, 0, 1);
    }


    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Death(string name, int actor)
    {
        //現在のHPを０に調整
        currentHp = 0;

        //死亡関数を呼び出し
        spawnManager.Die();

        //死亡UIを更新
        uIManager.UpdateDeathUI(name);

        //自分のデス数を上昇(自分の識別番号、デス、加算数値)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //撃ってきた相手のキル数を上昇(撃ってきた敵の識別番号、キル、加算数値)
        gameManager.ScoreGet(actor, 0, 1);
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