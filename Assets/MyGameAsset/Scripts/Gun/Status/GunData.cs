using UnityEngine;

/// <summary>
/// 銃のパラメータクラス
/// </summary>
[CreateAssetMenu(fileName = "NewGunData", menuName = "MyFPSGameDate/Gun Data", order = 0)]
public class GunData : ScriptableObject
{
    [Header(" 弾薬関連 ")]
    [SerializeField] int maxAmmunition;
    [SerializeField] int maxAmmoClip;
    int ammunition;
    int ammoClip;

    [Header(" 射撃関連 ")]
    [SerializeField] int shotDamage         = 10;
    [SerializeField] float shootInterval    = 0.1f;
    [SerializeField] float adsZoom          = 2.0f;
    [SerializeField] float adsSpeed         = 0.5f;

    /// <summary>
    /// 所持弾薬数の最大数
    /// </summary>
    public int MaxAmmunition => maxAmmunition;

    /// <summary>
    /// マガジン1つあたりの最大弾薬数
    /// </summary>
    public int MaxAmmoClip => maxAmmoClip;

    /// <summary>
    /// 現在の所持弾薬数
    /// </summary>
    public int Ammunition
    {
        get { return ammunition; }
        set { ammunition = Mathf.Clamp(value, 0, maxAmmunition); }
    }

    /// <summary>
    /// 現在のマガジン内弾薬数
    /// </summary>
    public int AmmoClip
    {
        get { return ammoClip; }
        set { ammoClip = Mathf.Clamp(value, 0, maxAmmoClip); }
    }

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
}