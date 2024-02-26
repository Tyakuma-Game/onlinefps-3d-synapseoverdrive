using Photon.Pun;
using UnityEngine;

/// <summary>
/// アニメーションを管理するクラス
/// </summary>
public class PlayerAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator playerAnimator;

    // アニメーションパラメータのハッシュ
    const string HASH_DAMAGE        = "Damage";
    const string HASH_HP            = "HP";
    const string HASH_ATTACK_TYPE   = "AttackType";
    const string HASH_ATTACK        = "Attack";
    const string HASH_MOVE_SPEED    = "MoveSpeed";
    const string HASH_IS_GROUND     = "IsGround";
    const string HASH_WEAPON_CHANGE = "WeaponChange";

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

        PlayerController.OnHPChanged += SetCurrentHP;
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

        PlayerController.OnHPChanged -= SetCurrentHP;
    }

    /// <summary>
    /// 移動速度の更新
    /// </summary>
    /// <param name="speed">移動速度</param>
    void UpdateMoveSpeed(float speed) =>
        playerAnimator.SetFloat(HASH_MOVE_SPEED, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// 地面接触状態の更新
    /// </summary>
    /// <param name="isGround">地面接触状態</param>
    void OnGroundContactChange(bool isGround) =>
        playerAnimator.SetBool(HASH_IS_GROUND, isGround);

    /// <summary>
    /// 被弾
    /// </summary>
    void OnDamage() =>
        playerAnimator.SetTrigger(HASH_DAMAGE);

    /// <summary>
    /// HPの更新
    /// </summary>
    /// <param name="hp">HP</param>
    void SetCurrentHP(int hp) =>
        playerAnimator.SetInteger(HASH_HP, hp);

    /// <summary>
    /// 武器交換
    /// </summary>
    void OnWeaponChange() =>
        playerAnimator.SetTrigger(HASH_WEAPON_CHANGE);

    /// <summary>
    /// 銃発射
    /// </summary>
    /// <param name="attackType">銃の種類</param>
    void OnGunShot(int attackType)
    {
        playerAnimator.SetInteger(HASH_ATTACK_TYPE, attackType);
        playerAnimator.SetTrigger(HASH_ATTACK);
    }
}