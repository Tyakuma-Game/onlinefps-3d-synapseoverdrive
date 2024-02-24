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

    
    /// <summary>
    /// ƒXƒe[ƒ^ƒX‰Šú‰»
    /// </summary>
    public void Init()
    {
        currentHp = playerConstants.MaxHP;            // ‘Ì—Í
        activeMoveSpeed = playerConstants.WalkSpeed;  // ˆÚ“®‘¬“x
        activeJumpForth = playerConstants.JumpForce;  // ƒWƒƒƒ“ƒv—Í
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
}