using Guns;
using Photon.Pun;
using UnityEngine;

public class GunAnimator : MonoBehaviourPunCallbacks
{
    [SerializeField] Animator gunAnimator;

    //string hashDamage = "Damage";
    //string hashAttackType = "AttackType";
    //string hashAttack = "Attack";
    string hashMoveSpeed = "MoveSpeed";
    string hashIsZoom = "IsZoom";
    //string hashWeaponChange = "WeaponChange";

    void Start()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // 処理登録
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;
        PlayerGunController.OnGunZoomStateChanged += GunZoomStateChange;
    }

    void OnDestroy()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // 処理解除
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
        PlayerGunController.OnGunZoomStateChanged -= GunZoomStateChange;
    }

    /// <summary>
    /// 現在の移動速度更新
    /// </summary>
    /// <param name="speed">現在の移動速度</param>
    void UpdateMoveSpeed(float speed) =>
        gunAnimator.SetFloat(hashMoveSpeed, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// ズーム状態変更
    /// </summary>
    /// <param name="isZoom">ズーム中なのかどうか</param>
    void GunZoomStateChange(bool isZoom) =>
        gunAnimator.SetBool(hashIsZoom, isZoom);
}