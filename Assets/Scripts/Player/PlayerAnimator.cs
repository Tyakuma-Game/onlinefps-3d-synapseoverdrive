using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// プレイヤーのアニメーション管理クラス
/// </summary>
public class PlayerAnimator : MonoBehaviourPunCallbacks
{
    [Header("参照")]

    [Tooltip("プレイヤーの移動クラス")]
    [SerializeField] PlayerMovement playerMovement;

    [Tooltip("Playerのアニメーター")]
    [SerializeField] Animator animator;


    void Update()
    {
        // 自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //アニメーションの状態更新
        AnimatorUpdate();
    }


    /// <summary>
    /// アニメーションの状態更新
    /// </summary>
    void AnimatorUpdate()
    {
        //歩き判定
        animator.SetBool("walk", playerMovement.GetMoveDir() != Vector3.zero);

        //走り判定
        animator.SetBool("run", Input.GetKey(KeyCode.LeftShift));
    }
}