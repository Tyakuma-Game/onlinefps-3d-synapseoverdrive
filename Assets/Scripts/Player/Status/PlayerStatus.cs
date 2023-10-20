using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// vC[ÌXe[^XlðÇ·éNX
/// </summary>
public class PlayerStatus: MonoBehaviour
{
    [Tooltip("vC[ÌèNX")]
    [SerializeField] PlayerConstants playerConstants;

    [Tooltip("JÌÊuIuWFNg")]
    [SerializeField] Transform viewPoint;

    [Tooltip("HPcÊ")]
    int currentHp;

    [Tooltip("Ú®¬x")]
    float activeMoveSpeed;

    [Tooltip("WvÍ")]
    Vector3 activeJumpForth;

    [Tooltip("»ÝÌAj[VóÔ")]
    PlayerAnimationState animationState;

    
    /// <summary>
    /// Xe[^Xú»
    /// </summary>
    public void Init()
    {
        currentHp = playerConstants.MaxHP;            // ÌÍ
        activeMoveSpeed = playerConstants.WalkSpeed;  // Ú®¬x
        activeJumpForth = playerConstants.JumpForce;  // WvÍ
        animationState = PlayerAnimationState.Idol;   // óÔ
    }

    /// <summary>
    /// _ÀWÌXV
    /// </summary>
    public void ViewPointUpdate(Transform transform)
    {
        viewPoint = transform;
    }

    /// <summary>
    /// _[W
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
    /// Ò@óÔÖJÚ
    /// </summary>
    public void IsIdol()
    {
        animationState = PlayerAnimationState.Idol;
    }

    /// <summary>
    /// à«óÔÖJÚ
    /// </summary>
    public void IsWalking()
    {
        activeMoveSpeed = playerConstants.WalkSpeed;
        animationState = PlayerAnimationState.Walk;
    }

    /// <summary>
    /// èóÔÖJÚ
    /// </summary>
    public void IsRunning()
    {
        activeMoveSpeed = playerConstants.RunSpeed;
        animationState = PlayerAnimationState.Run;
    }


    //|||||||||||||||||||||/
    // Qb^[
    //|||||||||||||||||||||/

    /// <summary>
    /// PLAYERÌèæ¾
    /// </summary>
    public PlayerConstants Constants
    {
        get { return playerConstants; }
    }

    /// <summary>
    /// »ÝÌHPÊ
    /// </summary>
    public int CurrentHP { get { return currentHp; } }

    /// <summary>
    /// »ÝÌÚ®¬x
    /// </summary>
    public float ActiveMoveSpeed { get { return activeMoveSpeed; } }

    /// <summary>
    /// »ÝÌWvÍ
    /// </summary>
    public Vector3 ActiveJumpForth { get { return activeJumpForth; } }

    /// <summary>
    /// »ÝÌAj[VóÔ
    /// </summary>
    public PlayerAnimationState AnimationState
    {
        get { return animationState; }
    }

    /// <summary>
    /// »ÝÌ_ÀW
    /// </summary>
    public Transform ViewPoint
    {
        get { return viewPoint; }
    }
}