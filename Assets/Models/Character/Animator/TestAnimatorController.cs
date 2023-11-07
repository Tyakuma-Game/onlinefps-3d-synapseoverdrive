using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType : int
{
    Short = 0,
    Normal = 1,
    Power = 2,
}

public interface ICharacterAnimationParam
{
    void Apply(float speed, float fallSpedd, bool isGround, int hp);

    void Attack(AttackType type);

    void Damage();
}




public class TestAnimatorController : MonoBehaviour
{
    //|||||||||||||||||||||/
    // s¾ª
    //|||||||||||||||||||||/

    [SerializeField]Animator animator;
    
    // _[WÖA
    string hashDamage = "Damage";
    string hashHP = "HP";

    // UÖA
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";

    // Ú®ÖA
    string hashMoveSpeed = "MoveSpeed";

    // JumpÖA
    string hashIsGround = "IsGround";

    // íð·
    string hashWeaponChange = "WeaponChange";


    //|||||||||||||||||||||/

    /// <summary>
    /// eXgpÌAj[V¬xüê
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