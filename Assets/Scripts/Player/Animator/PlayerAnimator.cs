using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// vC[ÌóÔê
/// </summary>
public enum PlayerAnimationState
{
    Idol,   // Ò@óÔ
    Walk,   // à«óÔ
    Run,    // èóÔ
    Jump    // WvóÔ
}

/// <summary>
/// vC[ÌAj[VÇNX
/// </summary>
public class PlayerAnimator : MonoBehaviour,IPlayerAnimator
{
    [Tooltip("PlayerÌAj[^[")]
    [SerializeField] Animator animator;

    /// <summary>
    /// Aj[VÌXV
    /// </summary>
    /// <param name="playerAnimationState">»ÝÌAj[VóÔ</param>
    public void AnimationUpdate(PlayerAnimationState playerAnimationState)
    {
        //à«»è
        animator.SetBool("walk", playerAnimationState == PlayerAnimationState.Walk);

        //è»è
        animator.SetBool("run", playerAnimationState == PlayerAnimationState.Run);
    }
}



//|||||||||||||||||||||/
// ¼§ìÌAnimator
//|||||||||||||||||||||/

//public interface ICharacterAnimationParam
//{
//    void Apply(float speed,float fallSpeed,bool isGround, int hp);
//    void Attack(ShotType type);
//    void Damage();
//}
//public enum ShotType : int
//{
//    OneShot = 0,    // ê­
//    ManyShot,       // ¡
//}

//public class AAA : ICharacterAnimationParam
//{
//    Animator animator;
//    int hashDamage;
//    int hashAttackType;
//    int hashHP;
//    bool hashIsGround;

//    public void Apply(float speed, float fallSpeed,bool isGround,int hp)
//    {
//        animator.SetInteger(hashHP,hp);
//        animator.SetBool("hashIsGround",isGround);
//        animator.SetFloat(hash)
//    }

//    public void Attack(ShotType type)
//    {
//        animator.SetInteger(hashAttackType , (int)type);
//        animator.SetTrigger(hashAttackType);
//    }


//    public void Damage()
//    {
//        animator.SetTrigger(hashDamage);
//    }
//}
//|||||||||||||||||||||/