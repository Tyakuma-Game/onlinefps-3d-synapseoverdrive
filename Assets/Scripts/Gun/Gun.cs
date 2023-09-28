using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("定数")]
    [Tooltip("射撃間隔")]
    public float shootInterval = .1f;
    
    [Tooltip("威力")]
    public int shotDamage;
    
    [Tooltip("覗き込み時のズーム")]
    public float adsZoom;
    
    [Tooltip("覗き込み時の速度")]
    public float adsSpeed;


    [Header("参照")]
    [Tooltip("弾痕のエフェクト")]
    public GameObject bulletImpact;


    [Header("サウンド関連")]

    [Tooltip("銃の発射音")]
    public AudioSource shoutSound;
    
    [Tooltip("薬莢落下音")]
    public AudioSource cartridgeCaseSound;
    
    [Tooltip("弾薬切れの音")]
    public AudioSource outOfAmmoSound;

    [Tooltip("リロードの音")]
    public AudioSource reloadingSound;//未完了


    /// <summary>
    /// 銃声を１発鳴らす
    /// </summary>
    public void SoundGunShot()
    {
        shoutSound.Play();                      //一度鳴らす
        Invoke("SoundGunCartridgeCase", 0.3F);  //薬莢の落下音を一定時間後に鳴らす
    }


    /// <summary>
    /// サブマシンガンのループON
    /// </summary>
    public void LoopON_SubmachineGun()
    {
        //音がなっていないなら
        if (!shoutSound.isPlaying)
        {
            shoutSound.loop = true;                 //ループON
            shoutSound.Play();                      //音を鳴らし始める
            Invoke("SoundGunCartridgeCase", 0.3F);  //薬莢の落下音を一定時間後に鳴らす
        }
    }


    /// <summary>
    /// サブマシンガンのループOFF
    /// </summary>
    public void LoopOFF_SubmachineGun()
    {
        shoutSound.loop = false;    //ループOFF
        shoutSound.Stop();          //音を止める
    }


    /// <summary>
    /// 弾切れの音を鳴らす
    /// </summary>
    public void SoundGunOutOfBullets()
    {
        //音が鳴ってないなら
        if (!outOfAmmoSound.isPlaying)
        {
            outOfAmmoSound.Play();  //一度鳴らす
        }
    }


    /// <summary>
    /// ピストルのリロード音
    /// </summary>
    public void SoundPistolReloading()
    {
        reloadingSound.Play();//一度鳴らす
    }


    /// <summary>
    /// ショットガンのリロード音
    /// </summary>
    public void SoundShotgunReloading()
    {
        reloadingSound.Play();//一度鳴らす
    }


    /// <summary>
    /// アサルトのリロード音
    /// </summary>
    public void SoundAssaultReloading()
    {
        reloadingSound.Play();//一度鳴らす
    }


    /// <summary>
    /// 薬莢の落下音を鳴らす
    /// </summary>
    public void SoundGunCartridgeCase()
    {
        //音が鳴ってないなら
        if (!cartridgeCaseSound.isPlaying)
        {
            cartridgeCaseSound.Play();  //一度鳴らす
        }
    }
}