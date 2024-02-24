using Photon.Pun;
using UnityEngine;

/// <summary>
/// 攻撃の種類
/// </summary>
public enum AttackType : int
{
    Short = 0,
    Normal = 1,
    Power = 2,
}

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
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // 処理解除
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange -= OnGroundContactChange;
        PlayerEvent.onDamage -= OnDamage;
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

    /// <summary>
    /// 武器交換のトリガーを立てる
    /// </summary>
    public void IsWeaponChange()
    {
        playerAnimator.SetTrigger(hashWeaponChange);
    }

    /// <summary>
    /// 攻撃アニメーションを再生
    /// </summary>
    /// <param name="typeID">攻撃の種類</param>
    public void Attack(AttackType typeID)
    {
        playerAnimator.SetInteger(hashAttackType, (int)typeID);
        playerAnimator.SetTrigger(hashAttack);
    }
}