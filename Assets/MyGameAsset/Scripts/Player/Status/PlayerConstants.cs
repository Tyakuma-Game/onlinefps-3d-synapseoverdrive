using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒvƒŒƒCƒ„[‚Ì’è”‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class PlayerConstants : MonoBehaviour
{
    //|||||||||||||||||||||/
    // ‹“_ŠÖ˜A
    //|||||||||||||||||||||/
    [Header("‹“_ŠÖ˜A")]

    [Tooltip("ƒJƒƒ‰‚ÌŒ³‚Ìi‚è”{—¦")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    /// <summary>
    /// Player‚ÌƒJƒƒ‰Œ³‚Ìi‚è”{—¦
    /// </summary>
    public float CameraApertureBaseFactor
    {
        get { return CAMERA_APERTURE_BASE_FACTOR; }
    }

    //|||||||||||||||||||||/
    // ‘Ì—ÍŠÖ˜A
    //|||||||||||||||||||||/
    [Header("‘Ì—ÍŠÖ˜A")]
    [Tooltip("Player‚ÌHPÅ‘å’l")]
    [SerializeField] int PLAYER_MAX_HP = 100;

    /// <summary>
    /// Player‚ÌHPÅ‘å’l
    /// </summary>
    public int MaxHP
    {
        get { return PLAYER_MAX_HP; }
    }

    //|||||||||||||||||||||/
    // ˆÚ“®ŠÖ˜A
    //|||||||||||||||||||||/
    [Header("ˆÚ“®ŠÖ˜A")]
    [Tooltip("Player‚Ì•à‚«‘¬“x")]
    [SerializeField] float PLAYER_WALK_SPEED = 4f;

    /// <summary>
    /// Player‚Ì•à‚«‘¬“x
    /// </summary>
    public float WalkSpeed
    {
        get { return PLAYER_WALK_SPEED; }
    }

    [Tooltip("Player‚Ì‘–‚è‘¬“x")]
    [SerializeField] float PLAYER_RUN_SPEED = 8f;

    /// <summary>
    /// Player‚Ì‘–‚è‘¬“x
    /// </summary>
    public float RunSpeed
    {
        get { return PLAYER_RUN_SPEED; }
    }

    [Tooltip("Player‚ÌƒWƒƒƒ“ƒv—Í")]
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0, 3f, 0);

    /// <summary>
    /// Player‚ÌƒWƒƒƒ“ƒv—Í
    /// </summary>
    public Vector3 JumpForce
    {
        get { return PLAYER_JUMP_FORTH; }
    }

    [Tooltip("Player‚Ì‰ñ“]‘¬“x")]
    [SerializeField] float ROTATION_SPEED = 5.0f;

    /// <summary>
    /// Player‚Ì‰ñ“]‘¬“x
    /// </summary>
    public float RotationSpeed
    {
        get { return ROTATION_SPEED; }
    }
}