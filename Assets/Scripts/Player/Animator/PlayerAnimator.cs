using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの状態一覧
/// </summary>
public enum PlayerAnimationState
{
    Idol,   // 待機状態
    Walk,   // 歩き状態
    Run,    // 走り状態
    Jump    // ジャンプ状態
}

/// <summary>
/// プレイヤーのアニメーション管理クラス
/// </summary>
public class PlayerAnimator : MonoBehaviour,IPlayerAnimator
{
    [Tooltip("Playerのアニメーター")]
    [SerializeField] Animator animator;

    /// <summary>
    /// アニメーションの更新処理
    /// </summary>
    /// <param name="playerAnimationState">現在選択中のアニメーション</param>
    public void AnimationUpdate(PlayerAnimationState playerAnimationState)
    {
        //歩き判定
        animator.SetBool("walk", playerAnimationState == PlayerAnimationState.Walk);

        //走り判定
        animator.SetBool("run", playerAnimationState == PlayerAnimationState.Run);
    }
}