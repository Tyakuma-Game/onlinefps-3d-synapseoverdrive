using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのステータス値を管理するクラス
/// </summary>
public class PlayerStatus: MonoBehaviour
{
    [Tooltip("プレイヤーの定数クラス")]
    [SerializeField] PlayerConstants playerConstants;

    [Tooltip("カメラの位置オブジェクト")]
    [SerializeField] Transform viewPoint;


    int currentHp;                          // 現在のHP
    float activeMoveSpeed;                  // 現在の移動速度
    Vector3 jumpForth;                      // ジャンプ力
    PlayerAnimationState animationState;    // 現在の状態

    
    /// <summary>
    /// ステータス初期化
    /// </summary>
    public void Init()
    {
        currentHp = playerConstants.MaxHP;            // 体力
        activeMoveSpeed = playerConstants.WalkSpeed;  // 移動速度
        jumpForth = playerConstants.JumpForce;        // ジャンプ力
        animationState = PlayerAnimationState.Idol;   // 状態
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }
    }

    /// <summary>
    /// 待機状態へ遷移
    /// </summary>
    public void IsIdol()
    {
        animationState = PlayerAnimationState.Idol;
    }

    /// <summary>
    /// 歩き状態へ遷移
    /// </summary>
    public void IsWalking()
    {
        activeMoveSpeed = playerConstants.WalkSpeed;
        animationState = PlayerAnimationState.Walk;
    }

    /// <summary>
    /// 走り状態へ遷移
    /// </summary>
    public void IsRunning()
    {
        activeMoveSpeed = playerConstants.RunSpeed;
        animationState = PlayerAnimationState.Run;
    }

    /// <summary>
    /// PLAYERの定数取得
    /// </summary>
    public PlayerConstants Constants
    {
        get { return playerConstants; }
    }

    /// <summary>
    /// 現在のHP量
    /// </summary>
    public int CurrentHP { get { return currentHp; } }

    /// <summary>
    /// 現在の移動速度
    /// </summary>
    public float ActiveMoveSpeed { get { return activeMoveSpeed; } }

    /// <summary>
    /// 現在のアニメーション状態
    /// </summary>
    public PlayerAnimationState AnimationState
    {
        get { return animationState; }
    }

    /// <summary>
    /// 現在の視点座標
    /// </summary>
    public Transform ViewPoint
    {
        get { return viewPoint; }
    }
}