using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Playerのジャンプに関するクラス
/// </summary>
public class PlayerJump : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// 地面に接触しているかどうかを通知するイベント
    /// </summary>
    public static event Action<bool> OnGroundContactChange;

    [Header(" Settings ")]
    [SerializeField] Vector3 jumpForce;
    [SerializeField] LayerMask groundLayers;

    Rigidbody rb;
    InputAction jumpAction;
    bool isGround = true;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // 取得
        rb = GetComponent<Rigidbody>();
        jumpAction = InputManager.Controls.Player.Jump;

        // 処理登録
        jumpAction.started += OnJump;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;
        
        // 処理解除
        jumpAction.started -= OnJump;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 地面に接触していない & 衝突したオブジェクトが指定された地面のレイヤーに含まれているかチェック
        if (!isGround && ((1 << collision.gameObject.layer) & groundLayers) != 0)
        {
            // 変更と通知
            isGround = true;
            OnGroundContactChange?.Invoke(isGround);
        }
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void OnJump(InputAction.CallbackContext context)
    {
        if (isGround)
        {
            rb.AddForce(jumpForce, ForceMode.VelocityChange);

            // 変更と通知
            isGround = false;
            OnGroundContactChange?.Invoke(isGround);
        }
    }
}