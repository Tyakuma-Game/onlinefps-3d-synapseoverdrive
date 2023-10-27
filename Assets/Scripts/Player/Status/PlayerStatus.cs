using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒvƒŒƒCƒ„[‚ÌƒXƒe[ƒ^ƒX’l‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class PlayerStatus: MonoBehaviour
{
    [Tooltip("ƒvƒŒƒCƒ„[‚Ì’è”ƒNƒ‰ƒX")]
    [SerializeField] PlayerConstants playerConstants;

    [Tooltip("HPc—Ê")]
    int currentHp;

    [Tooltip("ˆÚ“®‘¬“x")]
    float activeMoveSpeed;

    [Tooltip("ƒWƒƒƒ“ƒv—Í")]
    Vector3 activeJumpForth;

    [Tooltip("Œ»İ‚ÌƒAƒjƒ[ƒVƒ‡ƒ“ó‘Ô")]
    PlayerAnimationState animationState;

    
    /// <summary>
    /// ƒXƒe[ƒ^ƒX‰Šú‰»
    /// </summary>
    public void Init()
    {
        currentHp = playerConstants.MaxHP;            // ‘Ì—Í
        activeMoveSpeed = playerConstants.WalkSpeed;  // ˆÚ“®‘¬“x
        activeJumpForth = playerConstants.JumpForce;  // ƒWƒƒƒ“ƒv—Í
        animationState = PlayerAnimationState.Idol;   // ó‘Ô
    }

    /// <summary>
    /// ƒ_ƒ[ƒWˆ—
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
    /// ‘Ò‹@ó‘Ô‚Ö‘JˆÚ
    /// </summary>
    public void IsIdol()
    {
        animationState = PlayerAnimationState.Idol;
    }

    /// <summary>
    /// •à‚«ó‘Ô‚Ö‘JˆÚ
    /// </summary>
    public void IsWalking()
    {
        activeMoveSpeed = playerConstants.WalkSpeed;
        animationState = PlayerAnimationState.Walk;
    }

    /// <summary>
    /// ‘–‚èó‘Ô‚Ö‘JˆÚ
    /// </summary>
    public void IsRunning()
    {
        activeMoveSpeed = playerConstants.RunSpeed;
        animationState = PlayerAnimationState.Run;
    }


    //|||||||||||||||||||||/
    // ƒQƒbƒ^[
    //|||||||||||||||||||||/

    /// <summary>
    /// PLAYER‚Ì’è”æ“¾
    /// </summary>
    public PlayerConstants Constants
    {
        get { return playerConstants; }
    }

    /// <summary>
    /// Œ»İ‚ÌHP—Ê
    /// </summary>
    public int CurrentHP { get { return currentHp; } }

    /// <summary>
    /// Œ»İ‚ÌˆÚ“®‘¬“x
    /// </summary>
    public float ActiveMoveSpeed { get { return activeMoveSpeed; } }

    /// <summary>
    /// Œ»İ‚ÌƒWƒƒƒ“ƒv—Í
    /// </summary>
    public Vector3 ActiveJumpForth { get { return activeJumpForth; } }

    /// <summary>
    /// Œ»İ‚ÌƒAƒjƒ[ƒVƒ‡ƒ“ó‘Ô
    /// </summary>
    public PlayerAnimationState AnimationState
    {
        get { return animationState; }
    }
}