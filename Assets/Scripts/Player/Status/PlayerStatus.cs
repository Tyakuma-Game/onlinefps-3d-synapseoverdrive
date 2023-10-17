using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのステータス値を管理するクラス
/// </summary>
public class PlayerStatus
{
    [Tooltip("PlayerのHP最大値")]
    [SerializeField] int PLAYER_MAX_HP = 100;

    [Tooltip("Playerの歩き速度")]
    [SerializeField] float PLAYER_WALK_SPEED = 4f;

    [Tooltip("Playerの走り速度")]
    [SerializeField] float PLAYER_RUN_SPEED = 8f;

    [Tooltip("Playerのジャンプ力")]
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0, 3f, 0);

    [Tooltip("Playerの回転速度")]
    [Range(1f, 10f)]
    [SerializeField] float ROTATION_SPEED = 5.0f;


    //[Tooltip("視点移動の速度")]
    //[SerializeField] float MOUSE_SENSITIVITY = 1f;

    //[Tooltip("視点の上下回転範囲")]
    //[SerializeField] float VERTICAL_ROTATION_RANGE = 60f;

    int currentHp;                          // 現在のHP
    float activeMoveSpeed;                  // 現在の移動速度
    Vector3 jumpForth;                      // ジャンプ力
    PlayerAnimationState animationState;    // 現在の状態

    /// <summary>
    /// HP最大値
    /// </summary>
    public int MAX_HP
    {
        get { return PLAYER_MAX_HP; }
    }

    /// <summary>
    /// 回転速度
    /// </summary>
    public float ROTA_SPEED
    {
        get { return ROTATION_SPEED; }
    }

    /// <summary>
    /// 現在のHP
    /// </summary>
    public int CurrentHP
    {
        get { return currentHp; }
    }

    /// <summary>
    /// 現在の移動速度
    /// </summary>
    public float ActiveMoveSpeed
    {
        get { return activeMoveSpeed; }
    }

    /// <summary>
    /// ジャンプ力
    /// </summary>
    public Vector3 JumpForth
    {
        get { return jumpForth; }
    }

    /// <summary>
    /// 現在のアニメーション状態
    /// </summary>
    public PlayerAnimationState AnimationState
    {
        get { return animationState; }
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
    /// ステータス初期化
    /// </summary>
    public void Init()
    {
        currentHp = PLAYER_MAX_HP;                   // 体力
        activeMoveSpeed = PLAYER_WALK_SPEED;         // 移動速度
        jumpForth = PLAYER_JUMP_FORTH;               // ジャンプ力
        animationState = PlayerAnimationState.Idol;  // 状態
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
        activeMoveSpeed = PLAYER_WALK_SPEED;
        animationState = PlayerAnimationState.Walk;
    }

    /// <summary>
    /// 走り状態へ遷移
    /// </summary>
    public void IsRunning()
    {
        activeMoveSpeed = PLAYER_RUN_SPEED;
        animationState = PlayerAnimationState.Run;
    }
}