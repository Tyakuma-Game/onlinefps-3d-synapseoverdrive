using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 銃のStatusを管理するクラス
/// </summary>
public class GunStatus : MonoBehaviour
{
    [SerializeField] AudioSource shotSE;    //　銃の発砲音
    [SerializeField] Light shotLight;       //　銃の光
    [SerializeField] GameObject shotEffect; //　弾が当たった時のパーティクル

    public AudioSource GetShotSE()
    {
        return shotSE;
    }

    public Light GetShotLight()
    {
        return shotLight;
    }

    public void ActiveShotEffect()
    {
        shotEffect.SetActive(true);
    }

    public void ShotEffectNotActive()
    {
        shotEffect.SetActive(false);
    }
}