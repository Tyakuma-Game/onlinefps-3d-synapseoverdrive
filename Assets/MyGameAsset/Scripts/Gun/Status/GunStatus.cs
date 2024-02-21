using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// eΜStatusπΗ·ιNX
/// </summary>
public class GunStatus : MonoBehaviour
{
    //|||||||||||||||||||||/
    // θ
    //|||||||||||||||||||||/

    [Header("θ")]
    [Tooltip("ΛΤu")]
    [SerializeField] float shootInterval = 0.1f;

    [Tooltip("ΠΝ")]
    [SerializeField] int shotDamage;

    [Tooltip("`«έΜY[")]
    [SerializeField] float adsZoom;

    [Tooltip("`«έΜ¬x")]
    [SerializeField] float adsSpeed;



    //|||||||||||||||||||||/
    // Qb^[
    //|||||||||||||||||||||/

    /// <summary>
    /// eΜ­ΛC^[oΤ
    /// </summary>
    public float ShootInterval
    {
        get { return shootInterval; }
    }

    /// <summary>
    /// eΜΠΝ
    /// </summary>
    public int ShotDamage
    {
        get { return shotDamage; }
    }

    /// <summary>
    /// Y[{¦
    /// </summary>
    public float AdsZoom
    {
        get { return adsZoom; }
    }

    /// <summary>
    /// Y[¬x
    /// </summary>
    public float AdsSpeed
    {
        get { return adsSpeed; }
    }

}