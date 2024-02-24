using UnityEngine;

/// <summary>
/// 銃のパラメータクラス
/// </summary>
[CreateAssetMenu(fileName = "NewGunData", menuName = "MyFPSGameDate/Gun Data", order = 0)]
public class GunData : ScriptableObject
{
    [Header(" 弾薬関連 ")]
    [SerializeField] int maxAmmunition; // 所持弾薬最大数
    [SerializeField] int maxAmmoClip;   // １マガジン当たりの最大弾薬数

    [Header(" 射撃関連 ")]
    [SerializeField] int shotDamage = 10;           // 一発当たりのダメージ量
    [SerializeField] float shootInterval = 0.1f;    // 銃の発射間隔
    [SerializeField] float adsZoom = 2.0f;          // ズーム倍率
    [SerializeField] float adsSpeed = 0.5f;         // ズーム速度

    [Header(" エフェクト関連 ")]
    [SerializeField] GameObject playerHitEffect;    // プレイヤーに当った際に生成するオブジェクト
    [SerializeField] GameObject nonPlayerHitEffect; // プレイヤーではないオブジェクトに被弾時に生成するオブジェクト

    /// <summary>
    /// 所持弾薬数の最大数
    /// </summary>
    public int MaxAmmunition => maxAmmunition;

    /// <summary>
    /// マガジン1つあたりの最大弾薬数
    /// </summary>
    public int MaxAmmoClip => maxAmmoClip;

    /// <summary>
    /// 1発のダメージ量
    /// </summary>
    public int ShotDamage => shotDamage;

    /// <summary>
    /// 射撃間隔（秒）
    /// </summary>
    public float ShootInterval => shootInterval;

    /// <summary>
    /// 照準時のズーム倍率
    /// </summary>
    public float AdsZoom => adsZoom;

    /// <summary>
    /// 照準の動作速度
    /// </summary>
    public float AdsSpeed => adsSpeed;

    /// <summary>
    /// プレイヤーに弾が当った際に生成するエフェクト
    /// </summary>
    public GameObject PlayerHitEffect => playerHitEffect;

    /// <summary>
    /// プレイヤーではないオブジェクトに弾が当った際に生成するエフェクト
    /// </summary>
    public GameObject NonPlayerHitEffect => nonPlayerHitEffect;
}