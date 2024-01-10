using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// vC[ฬ่๐วท้NX
/// </summary>
public class PlayerConstants : MonoBehaviour
{
    //|||||||||||||||||||||/
    // _ึA
    //|||||||||||||||||||||/
    [Header("_ึA")]

    [Tooltip("Jฬณฬi่{ฆ")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    /// <summary>
    /// PlayerฬJณฬi่{ฆ
    /// </summary>
    public float CameraApertureBaseFactor
    {
        get { return CAMERA_APERTURE_BASE_FACTOR; }
    }

    [Tooltip("Playerฬ_ใบ๑]ออ")]
    [SerializeField] float VERTICAL_ROTATION_RANGE = 60f;

    /// <summary>
    /// Playerฬ_ใบ๑]ออ
    /// </summary>
    public float VerticalRotationRange
    {
        get { return VERTICAL_ROTATION_RANGE; }
    }

    [Tooltip("_ฺฎฬฌx")]
    [SerializeField] float MOUSE_SENSITIVITY = 1f;

    /// <summary>
    /// _ฺฎฬฌx
    /// </summary>
    public float MouseSensitivity
    {
        get { return MOUSE_SENSITIVITY; }
    }

    //|||||||||||||||||||||/
    // ฬอึA
    //|||||||||||||||||||||/
    [Header("ฬอึA")]
    [Tooltip("PlayerฬHPลๅl")]
    [SerializeField] int PLAYER_MAX_HP = 100;

    /// <summary>
    /// PlayerฬHPลๅl
    /// </summary>
    public int MaxHP
    {
        get { return PLAYER_MAX_HP; }
    }

    //|||||||||||||||||||||/
    // ฺฎึA
    //|||||||||||||||||||||/
    [Header("ฺฎึA")]
    [Tooltip("Playerฬเซฌx")]
    [SerializeField] float PLAYER_WALK_SPEED = 4f;

    /// <summary>
    /// Playerฬเซฌx
    /// </summary>
    public float WalkSpeed
    {
        get { return PLAYER_WALK_SPEED; }
    }

    [Tooltip("Playerฬ่ฌx")]
    [SerializeField] float PLAYER_RUN_SPEED = 8f;

    /// <summary>
    /// Playerฬ่ฌx
    /// </summary>
    public float RunSpeed
    {
        get { return PLAYER_RUN_SPEED; }
    }

    [Tooltip("PlayerฬWvอ")]
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0, 3f, 0);

    /// <summary>
    /// PlayerฬWvอ
    /// </summary>
    public Vector3 JumpForce
    {
        get { return PLAYER_JUMP_FORTH; }
    }

    [Tooltip("Playerฬ๑]ฌx")]
    [SerializeField] float ROTATION_SPEED = 5.0f;

    /// <summary>
    /// Playerฬ๑]ฌx
    /// </summary>
    public float RotationSpeed
    {
        get { return ROTATION_SPEED; }
    }
}