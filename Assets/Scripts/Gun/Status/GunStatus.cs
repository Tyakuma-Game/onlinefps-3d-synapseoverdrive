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

    public float ShootInterval
    {
        get { return shootInterval; }
    }
    public int ShotDamage
    {
        get { return shotDamage; }
    }
    public float AdsZoom
    {
        get { return adsZoom; }
    }
    public float AdsSpeed
    {
        get { return adsSpeed; }
    }

    //|||||||||||||||||||||/
    // QΖ
    //|||||||||||||||||||||/
    [Header("QΖ")]
    [SerializeField] AudioSource shotSE;    //@eΜ­CΉ
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
    /// eΜ­CΉπζΎ
    /// </summary>
    /// <returns>eΜ­CΉ</returns>
    public AudioSource GetShotSE()
    {
        return shotSE;
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