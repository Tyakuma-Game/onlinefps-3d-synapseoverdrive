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
    // QÆ
    //|||||||||||||||||||||/
    [Header("QÆ")]
    [SerializeField] AudioSource shotSE;    //@e‚Ì”­–C‰¹
    [SerializeField] Light shotLight;       //@”­–C‚ÌeŒõ
    [SerializeField] GameObject shotEffect; //@”­–C‚Ìƒp[ƒeƒBƒNƒ‹
    [SerializeField] GameObject hitEffect;  //@’e‚ª“–‚½‚Á‚½‚Ìƒp[ƒeƒBƒNƒ‹

    public void ActiveShotEffect()
    {
        shotEffect.SetActive(true);
    }

    public void ShotEffectNotActive()
    {
        shotEffect.SetActive(false);
    }



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

    /// <summary>
    /// e‚Ì”­–C‰¹‚ğæ“¾
    /// </summary>
    /// <returns>e‚Ì”­–C‰¹</returns>
    public AudioSource GetShotSE()
    {
        return shotSE;
    }

    /// <summary>
    /// e”­Ë‚Ì‰‰o—pƒ‰ƒCƒg‚ğæ“¾
    /// </summary>
    /// <returns>e”­Ë‚Ì‰‰o—pƒ‰ƒCƒg</returns>
    public Light GetShotLight()
    {
        return shotLight;
    }

    /// <summary>
    /// e”­–C‚ÌƒGƒtƒFƒNƒg‚ğæ“¾
    /// </summary>
    /// <returns>e”­–C‚ÌƒGƒtƒFƒNƒg</returns>
    public GameObject GetShotEffect()
    {
        return shotEffect;
    }

    /// <summary>
    /// ’e’…’e‚ÌEffect‚ğæ“¾
    /// </summary>
    /// <returns>’e’…’e‚ÌEffect</returns>
    public GameObject GetHitEffect()
    {
        return hitEffect;
    }
}