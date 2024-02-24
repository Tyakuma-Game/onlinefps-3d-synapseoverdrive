using UnityEngine;
using Photon.Pun;
using MiniMap;
using System;

public static class PlayerEvent
{
    public static Action onDamage;
    public static Action onSpawn;
    public static Action onDisappear;
}

/// <summary>
/// Player管理クラス
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    //−−−−−−−−−−−−−−−−−−−−−/
    //　効率化中
    //−−−−−−−−−−−−−−−−−−−−−/

    [Tooltip("プレイヤーのステータス情報")]
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] EnemyIconController enemyIcon;

    [Tooltip("Playerのアニメーション処理")]
    PlayerAnimator playerAnimator;

    [SerializeField] PlayerSoundManager playerSoundManager;


    // 入力システム
    [Tooltip("キーボードの入力処理")]
    KeyBoardInput keyBoardInput;
   

    IMouseCursorLock mouseCursorLock;

   
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

    void Start()
    {
        // 指定時間後に演出を停止させる
        Invoke("SpawnEffectNotActive", 1.5f);

        //自分以外の場合は
        if (!photonView.IsMine)
        {
            enemyIcon.SetIconVisibility(true);
            //処理終了
            return;
        }
        enemyIcon.SetIconVisibility(false);
        MiniMapController.instance.SetMiniMapTarget(this.transform);

        // 入力システム
        keyBoardInput = GetComponent<KeyBoardInput>();
        mouseCursorLock = GetComponent<IMouseCursorLock>();
        mouseCursorLock.LockScreen();

        playerAnimator = GetComponent<PlayerAnimator>();
        playerStatus.Init();

        //HPスライダー反映
        UIManager.instance.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);

        // 現在のHPをセット
        playerAnimator.SetCurrentHP(playerStatus.CurrentHP);
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
        }

        //−−−−−−−−−−−−−−−−−−−−−/
        // PLAYER処理
        //−−−−−−−−−−−−−−−−−−−−−/
        //{
        //    // 移動　それぞれにアニメーション設定する処理をやって適用する感じにやる？　それをアクションに渡して移動のやつが呼び出すなど
        //    Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
        //    if (moveDirection != Vector3.zero)
        //    {
        //    }
        //    else
        //    {
        //        playerStatus.IsIdol();
        //    }
        //}

        //if (playerStatus.AnimationState == PlayerAnimationState.Run)
        //{
        //    UIManager.instance.IsRunning(); //Event登録型に変更する
        //}
        //else
        //{
        //    UIManager.instance.IsNotRunning();
        //}
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
            playerStatus.OnDamage(damage);

            // HP更新処理
            playerAnimator.SetCurrentHP(playerStatus.CurrentHP);

            // 死亡のその他処理
            PlayerEvent.onDamage?.Invoke();

            //現在のHPが0以下の場合
            if (playerStatus.CurrentHP <= 0 && isShowDeath == false)
            {
                //死亡関数を呼ぶ
                Death(name, actor);
            }

            //HPをスライダーに反映
            UIManager.instance.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
        }
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Death(string name, int actor)
    {
        //死亡UIを更新
        UIManager.instance.UpdateDeathUI(name);

        //自分のデス数を上昇(自分の識別番号、デス、加算数値)
        GameManager.instance.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //撃ってきた相手のキル数を上昇(撃ってきた敵の識別番号、キル、加算数値)
        GameManager.instance.ScoreGet(actor, 0, 1);

        // 死亡演出変更
        isShowDeath = true;

        // 消滅パーティクル出現
        photonView.RPC("SpawnEffectActive",RpcTarget.All);

        //死亡関数を呼び出し
        SpawnManager.instance.StartRespawnProcess();
    }

    /// <summary>
    /// Playerの始末処理
    /// </summary>
    public void OutGame()
    {
        GameManager.instance.OutPlayerGet(PhotonNetwork.LocalPlayer.ActorNumber); // プレイヤーデータ削除
        PhotonNetwork.AutomaticallySyncScene = false;                             // 同期切断
        PhotonNetwork.LeaveRoom();                                                // ルーム退出
    }
}