using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒvƒŒƒCƒ„[‚Ìó‘Ôˆê——
/// </summary>
public enum PlayerAnimationState
{
    Idol,   // ‘Ò‹@ó‘Ô
    Walk,   // •à‚«ó‘Ô
    Run,    // ‘–‚èó‘Ô
    Jump    // ƒWƒƒƒ“ƒvó‘Ô
}

/// <summary>
/// ƒvƒŒƒCƒ„[‚ÌƒAƒjƒ[ƒVƒ‡ƒ“ŠÇ—ƒNƒ‰ƒX
/// </summary>
public class PlayerAnimator : MonoBehaviour,IPlayerAnimator
{
    [Tooltip("Player‚ÌƒAƒjƒ[ƒ^[")]
    [SerializeField] Animator animator;

    /// <summary>
    /// ƒAƒjƒ[ƒVƒ‡ƒ“‚ÌXVˆ—
    /// </summary>
    /// <param name="playerAnimationState">Œ»İ‚ÌƒAƒjƒ[ƒVƒ‡ƒ“ó‘Ô</param>
    public void AnimationUpdate(PlayerAnimationState playerAnimationState)
    {
        //•à‚«”»’è
        animator.SetBool("walk", playerAnimationState == PlayerAnimationState.Walk);

        //‘–‚è”»’è
        animator.SetBool("run", playerAnimationState == PlayerAnimationState.Run);
    }
}



//|||||||||||||||||||||/
// ‰¼§ì’†‚ÌAnimator
//|||||||||||||||||||||/

//public interface ICharacterAnimationParam
//{
//    void Apply(float speed,float fallSpeed,bool isGround, int hp);
//    void Attack(ShotType type);
//    void Damage();
//}
//public enum ShotType : int
//{
//    OneShot = 0,    // ˆê”­
//    ManyShot,       // •¡”
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