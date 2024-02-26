using Guns;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// アニメーションを管理するクラス
/// </summary>
public class PlayerAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator playerAnimator;
    
    // 各要素の接続パス
    string hashDamage = "Damage";
    string hashHP = "HP";
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";
    string hashMoveSpeed = "MoveSpeed";
    string hashIsGround = "IsGround";
    string hashWeaponChange = "WeaponChange";

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // 処理登録
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange += OnGroundContactChange;
        PlayerEvent.onDamage += OnDamage;

        PlayerGunController.OnWeaponChangeCallback += OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback += OnGunShot;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // 処理解除
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange -= OnGroundContactChange;
        PlayerEvent.onDamage -= OnDamage;

        PlayerGunController.OnWeaponChangeCallback -= OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback -= OnGunShot;
    }

    /// <summary>
    /// 現在の移動速度更新
    /// </summary>
    /// <param name="speed">現在の移動速度</param>
    void UpdateMoveSpeed(float speed) =>
        playerAnimator.SetFloat(hashMoveSpeed, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// 地面に接触しているかどうかの更新
    /// </summary>
    /// <param name="isGround">接触しているか</param>
    void OnGroundContactChange(bool isGround) =>
        playerAnimator.SetBool(hashIsGround, isGround);

    /// <summary>
    /// 被弾
    /// </summary>
    void OnDamage() =>
        playerAnimator.SetTrigger(hashDamage);

    /// <summary>
    /// 武器交換
    /// </summary>
    void OnWeaponChange() =>
        playerAnimator.SetTrigger(hashWeaponChange);

    /// <summary>
    /// 銃発射
    /// </summary>
    /// <param name="attackType">銃の種類</param>
    void OnGunShot(int attackType)
    {
        playerAnimator.SetInteger(hashAttackType, attackType);
        playerAnimator.SetTrigger(hashAttack);
    }

    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    // リファクタリング中
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

    /// <summary>
    /// 現在のHPを入れる
    /// </summary>
    /// <param name="hp">現在のHP</param>
    public void SetCurrentHP(int hp)
    {
        playerAnimator.SetInteger(hashHP, hp);
    }
}