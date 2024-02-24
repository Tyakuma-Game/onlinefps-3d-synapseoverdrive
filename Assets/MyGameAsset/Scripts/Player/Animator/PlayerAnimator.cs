using Photon.Pun;
using UnityEngine;

/// <summary>
/// UŒ‚‚Ìí—Ş
/// </summary>
public enum AttackType : int
{
    Short = 0,
    Normal = 1,
    Power = 2,
}

/// <summary>
/// ƒAƒjƒ[ƒVƒ‡ƒ“‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class PlayerAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator playerAnimator;
    
    // Še—v‘f‚ÌÚ‘±ƒpƒX
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

        // ˆ—“o˜^
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange += OnGroundContactChange;
        PlayerEvent.onDamage += OnDamage;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // ˆ—‰ğœ
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange -= OnGroundContactChange;
        PlayerEvent.onDamage -= OnDamage;
    }

    /// <summary>
    /// Œ»İ‚ÌˆÚ“®‘¬“xXV
    /// </summary>
    /// <param name="speed">Œ»İ‚ÌˆÚ“®‘¬“x</param>
    void UpdateMoveSpeed(float speed) =>
        playerAnimator.SetFloat(hashMoveSpeed, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// ’n–Ê‚ÉÚG‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©‚ÌXV
    /// </summary>
    /// <param name="isGround">ÚG‚µ‚Ä‚¢‚é‚©</param>
    void OnGroundContactChange(bool isGround) =>
        playerAnimator.SetBool(hashIsGround, isGround);

    /// <summary>
    /// ”í’e
    /// </summary>
    void OnDamage() =>
        playerAnimator.SetTrigger(hashDamage);

    /// <summary>
    /// •ŠíŒğŠ·
    /// </summary>
    void OnWeaponChange() =>
        playerAnimator.SetTrigger(hashWeaponChange);

    

    //|||||||||||||||||||||||||||/
    // ƒŠƒtƒ@ƒNƒ^ƒŠƒ“ƒO’†
    //|||||||||||||||||||||||||||/

    /// <summary>
    /// Œ»İ‚ÌHP‚ğ“ü‚ê‚é
    /// </summary>
    /// <param name="hp">Œ»İ‚ÌHP</param>
    public void SetCurrentHP(int hp)
    {
        playerAnimator.SetInteger(hashHP, hp);
    }

    /// <summary>
    /// •ŠíŒğŠ·‚ÌƒgƒŠƒK[‚ğ—§‚Ä‚é
    /// </summary>
    public void IsWeaponChange()
    {
        playerAnimator.SetTrigger(hashWeaponChange);
    }

    /// <summary>
    /// UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“‚ğÄ¶
    /// </summary>
    /// <param name="typeID">UŒ‚‚Ìí—Ş</param>
    public void Attack(AttackType typeID)
    {
        playerAnimator.SetInteger(hashAttackType, (int)typeID);
        playerAnimator.SetTrigger(hashAttack);
    }
}