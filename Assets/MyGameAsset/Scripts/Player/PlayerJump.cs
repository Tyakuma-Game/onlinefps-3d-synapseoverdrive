using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Playerのジャンプに関するクラス
/// </summary>
public class PlayerJump : MonoBehaviour
{
    /// <summary>
    /// 地面との接触状態変更通知イベント
    /// </summary>
    public static event Action<bool> OnGroundContactChange;

    [Header(" Settings ")]
    [SerializeField] Vector3 jumpForce;
    [SerializeField] LayerMask groundLayers;

    Rigidbody rb;
    InputAction jumpAction;
    bool isGround = true;

    void Awake()
    {
        // 処理登録
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // 処理解除
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        if (jumpAction != null)
            jumpAction.started -= OnJump;
    }

    /// <summary>
    /// プレイヤーがインスタンス化された際に呼ばれる処理
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // 取得
        rb = GetComponent<Rigidbody>();
        jumpAction = InputManager.Controls.Player.Jump;

        // 登録
        jumpAction.started += OnJump;
    }

    /// <summary>
    /// 地面との接触判定を行い、接触状態が変化した場合はイベントを通じて通知
    /// </summary>
    /// <param name="collision">衝突したオブジェクトの情報</param>
    void OnCollisionEnter(Collision collision)
    {
        // 地面に接触していない & 衝突したオブジェクトが指定された地面のレイヤーに含まれているか
        if (!isGround && ((1 << collision.gameObject.layer) & groundLayers) != 0)
        {
            isGround = true;
            OnGroundContactChange?.Invoke(isGround);
        }
    }

    /// <summary>
    /// ジャンプの入力があった際に地面に接触している場合、ジャンプ処理を実行
    /// </summary>
    /// <param name="context">入力のコンテキスト</param>
    void OnJump(InputAction.CallbackContext context)
    {
        if (isGround)
        {
            rb.AddForce(jumpForce, ForceMode.VelocityChange);
            isGround = false;
            OnGroundContactChange?.Invoke(isGround);
        }
    }
}