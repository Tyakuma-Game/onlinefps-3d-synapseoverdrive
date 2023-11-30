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
    // QΖ
    //|||||||||||||||||||||/
    [Header("QΖ")]
    [SerializeField] AudioSource shotSE;    //@­CΉ
    [SerializeField] AudioSource notShotSE; //@eΨκ
    [SerializeField] Light shotLight;       //@­CΜeυ
    [SerializeField] GameObject shotEffect; //@­CΜp[eBN
    [SerializeField] GameObject hitEffect;  //@eͺ½Α½Μp[eBN

    public void ActiveShotEffect()
    {
        shotEffect.SetActive(true);
    }

    public void ShotEffectNotActive()
    {
        shotEffect.SetActive(false);
    }



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

    /// <summary>
    /// eΜ­CΉπζΎ
    /// </summary>
    /// <returns>eΜ­CΉ</returns>
    public AudioSource GetShotSE()
    {
        return shotSE;
    }

    public AudioSource GetNotShotSE()
    {
        return notShotSE;
    }

    /// <summary>
    /// e­ΛΜopCgπζΎ
    /// </summary>
    /// <returns>e­ΛΜopCg</returns>
    public Light GetShotLight()
    {
        return shotLight;
    }

    /// <summary>
    /// e­CΜGtFNgπζΎ
    /// </summary>
    /// <returns>e­CΜGtFNg</returns>
    public GameObject GetShotEffect()
    {
        return shotEffect;
    }

    /// <summary>
    /// eeΜEffectπζΎ
    /// </summary>
    /// <returns>eeΜEffect</returns>
    public GameObject GetHitEffect()
    {
        return hitEffect;
    }
}