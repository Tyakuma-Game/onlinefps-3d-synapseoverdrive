using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 銃のStatusを管理するクラス
/// </summary>
public class GunStatus : MonoBehaviour
{
    //−−−−−−−−−−−−−−−−−−−−−/
    // 定数
    //−−−−−−−−−−−−−−−−−−−−−/

    [Header("定数")]
    [Tooltip("射撃間隔")]
    [SerializeField] float shootInterval = 0.1f;

    [Tooltip("威力")]
    [SerializeField] int shotDamage;

    [Tooltip("覗き込み時のズーム")]
    [SerializeField] float adsZoom;

    [Tooltip("覗き込み時の速度")]
    [SerializeField] float adsSpeed;


    /* パーティクルらへんはアニメーションに組み込む形てかなり効率化出来る！　これで取りあえず、効率化してこのScriptを破棄する感じで良さげ！ */

    //−−−−−−−−−−−−−−−−−−−−−/
    // 参照
    //−−−−−−−−−−−−−−−−−−−−−/
    [Header("参照")]
    [SerializeField] AudioSource shotSE;    //　発砲音
    [SerializeField] AudioSource notShotSE; //　弾切れ
    [SerializeField] Light shotLight;       //　発砲時の銃光
    [SerializeField] GameObject shotEffect; //　発砲時のパーティクル
    [SerializeField] GameObject hitEffect;  //　弾が当たった時のパーティクル

    public void ActiveShotEffect()
    {
        shotEffect.SetActive(true);
    }

    public void ShotEffectNotActive()
    {
        shotEffect.SetActive(false);
    }



    //−−−−−−−−−−−−−−−−−−−−−/
    // ゲッター
    //−−−−−−−−−−−−−−−−−−−−−/

    /// <summary>
    /// 銃の発射インターバル時間
    /// </summary>
    public float ShootInterval
    {
        get { return shootInterval; }
    }

    /// <summary>
    /// 銃の威力
    /// </summary>
    public int ShotDamage
    {
        get { return shotDamage; }
    }

    /// <summary>
    /// ズーム倍率
    /// </summary>
    public float AdsZoom
    {
        get { return adsZoom; }
    }

    /// <summary>
    /// ズーム速度
    /// </summary>
    public float AdsSpeed
    {
        get { return adsSpeed; }
    }

    /// <summary>
    /// 銃の発砲音を取得
    /// </summary>
    /// <returns>銃の発砲音</returns>
    public AudioSource GetShotSE()
    {
        return shotSE;
    }

    public AudioSource GetNotShotSE()
    {
        return notShotSE;
    }

    /// <summary>
    /// 銃発射時の演出用ライトを取得
    /// </summary>
    /// <returns>銃発射時の演出用ライト</returns>
    public Light GetShotLight()
    {
        return shotLight;
    }

    /// <summary>
    /// 銃発砲時のエフェクトを取得
    /// </summary>
    /// <returns>銃発砲時のエフェクト</returns>
    public GameObject GetShotEffect()
    {
        return shotEffect;
    }

    /// <summary>
    /// 弾着弾時のEffectを取得
    /// </summary>
    /// <returns>弾着弾時のEffect</returns>
    public GameObject GetHitEffect()
    {
        return hitEffect;
    }
}