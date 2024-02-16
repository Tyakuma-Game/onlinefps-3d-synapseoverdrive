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
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    
    // ダメージ関連
    string hashDamage = "Damage";
    string hashHP = "HP";

    // 攻撃関連
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";

    // 移動関連
    string hashMoveSpeed = "MoveSpeed";

    // Jump関連
    string hashIsGround = "IsGround";

    // 武器交換
    string hashWeaponChange = "WeaponChange";

    /// <summary>
    /// 現在の移動速度を更新
    /// </summary>
    /// <param name="moveSpeed">移動速度</param>
    public void UpdateMoveSpeed(float moveSpeed)
    {
        playerAnimator.SetFloat(hashMoveSpeed, moveSpeed, 0.1f, Time.deltaTime);
    }

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
    /// 地面に着地しているかを設定
    /// </summary>
    /// <param name="isGround">現在着地しているか</param>
    public void IsGround(bool isGround)
    {
        playerAnimator.SetBool(hashIsGround, isGround);
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

    /// <summary>
    /// 被弾時のトリガーを立てる
    /// </summary>
    public void Damage()
    {
        playerAnimator.SetTrigger(hashDamage);
    }
}