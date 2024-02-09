using UnityEngine;

/// <summary>
/// UŒ‚‚Ìí—Ş
/// </summary>
public enum AttackType : int
{
    Short = 0,
    Normal = 1,
    Power = 2,
}

/// <summary>
/// ƒAƒjƒ[ƒVƒ‡ƒ“‚Ìˆ—‚ÉŠÖ‚·‚éƒCƒ“ƒ^[ƒtƒF[ƒX
/// </summary>
public interface ICharacterAnimationParam
{
    void Apply(float speed, float fallSpedd, bool isGround, int hp);

    void Attack(AttackType type);

    void Damage();
}



/// <summary>
/// ƒAƒjƒ[ƒVƒ‡ƒ“‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class TestAnimatorController : MonoBehaviour
{

    [SerializeField] Animator animator;
    
    // ƒ_ƒ[ƒWŠÖ˜A
    string hashDamage = "Damage";
    string hashHP = "HP";

    // UŒ‚ŠÖ˜A
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";

    // ˆÚ“®ŠÖ˜A
    string hashMoveSpeed = "MoveSpeed";

    // JumpŠÖ˜A
    string hashIsGround = "IsGround";

    // •ŠíŒğŠ·
    string hashWeaponChange = "WeaponChange";


    //|||||||||||||||||||||/

    /// <summary>
    /// ƒAƒjƒ[ƒVƒ‡ƒ“‘¬“x“ü‚ê
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void TestMove(float moveSpeed)
    {
        animator.SetFloat(hashMoveSpeed, moveSpeed, 0.1f, Time.deltaTime);
    }

    
    public void TestSetHP(int hp)
    {
        animator.SetInteger(hashHP, hp);
    }

    public void TestWeaponChange()
    {
        animator.SetTrigger(hashWeaponChange);
    }

    public void TestIsGround(bool isGround)
    {
        animator.SetBool(hashIsGround, isGround);
    }

    public void Attack(AttackType typeID)
    {
        animator.SetInteger(hashAttackType, (int)typeID);
        animator.SetTrigger(hashAttack);
    }

    public void Damage()
    {
        animator.SetTrigger(hashDamage);
    }
}