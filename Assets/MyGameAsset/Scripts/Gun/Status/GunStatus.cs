using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// e‚ÌStatus‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class GunStatus : MonoBehaviour
{
    //|||||||||||||||||||||/
    // ’è”
    //|||||||||||||||||||||/

    [Header("’è”")]
    [Tooltip("ËŒ‚ŠÔŠu")]
    [SerializeField] float shootInterval = 0.1f;

    [Tooltip("ˆĞ—Í")]
    [SerializeField] int shotDamage;

    [Tooltip("”`‚«‚İ‚ÌƒY[ƒ€")]
    [SerializeField] float adsZoom;

    [Tooltip("”`‚«‚İ‚Ì‘¬“x")]
    [SerializeField] float adsSpeed;



    //|||||||||||||||||||||/
    // ƒQƒbƒ^[
    //|||||||||||||||||||||/

    /// <summary>
    /// e‚Ì”­ËƒCƒ“ƒ^[ƒoƒ‹ŠÔ
    /// </summary>
    public float ShootInterval
    {
        get { return shootInterval; }
    }

    /// <summary>
    /// e‚ÌˆĞ—Í
    /// </summary>
    public int ShotDamage
    {
        get { return shotDamage; }
    }

    /// <summary>
    /// ƒY[ƒ€”{—¦
    /// </summary>
    public float AdsZoom
    {
        get { return adsZoom; }
    }

    /// <summary>
    /// ƒY[ƒ€‘¬“x
    /// </summary>
    public float AdsSpeed
    {
        get { return adsSpeed; }
    }

}