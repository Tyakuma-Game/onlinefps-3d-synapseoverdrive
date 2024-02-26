using Photon.Pun;
using UnityEngine;

/// <summary>
/// 銃のアニメーションに関する管理クラス
/// </summary>
public class GunAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator gunAnimator;

    // アニメーションパラメータのハッシュ
    const string HASH_ATTACK_TYPE   = "AttackType";
    const string HASH_ATTACK        = "Attack";
    const string HASH_MOVE_SPEED    = "MoveSpeed";
    const string HASH_IS_ZOOM       = "IsZoom";
    const string HASH_WEAPON_CHANGE = "WeaponChange";

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // 処理登録
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;

        PlayerGunController.OnGunZoomStateChanged += GunZoomStateChange;
        PlayerGunController.OnWeaponChangeCallback += OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback += OnGunShot;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // 処理解除
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;

        PlayerGunController.OnGunZoomStateChanged -= GunZoomStateChange;
        PlayerGunController.OnWeaponChangeCallback -= OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback -= OnGunShot;
    }

    /// <summary>
    /// 現在の移動速度更新
    /// </summary>
    /// <param name="speed">現在の移動速度</param>
    void UpdateMoveSpeed(float speed) =>
        gunAnimator.SetFloat(HASH_MOVE_SPEED, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// ズーム状態変更
    /// </summary>
    /// <param name="isZoom">ズーム中なのかどうか</param>
    void GunZoomStateChange(bool isZoom) =>
        gunAnimator.SetBool(HASH_IS_ZOOM, isZoom);

    /// <summary>
    /// 武器交換
    /// </summary>
    void OnWeaponChange() =>
        gunAnimator.SetTrigger(HASH_WEAPON_CHANGE);

    /// <summary>
    /// 銃発射
    /// </summary>
    /// <param name="attackType">銃の種類</param>
    void OnGunShot(int attackType)
    {
        gunAnimator.SetInteger(HASH_ATTACK_TYPE, attackType);
        gunAnimator.SetTrigger(HASH_ATTACK);
    }
}