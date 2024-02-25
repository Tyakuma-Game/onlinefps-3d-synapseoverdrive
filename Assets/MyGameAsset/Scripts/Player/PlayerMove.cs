using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using System;

/// <summary>
/// プレイヤーの移動を管理するクラス
/// </summary>
public class PlayerMove : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// 移動速度変更のCallback
    /// </summary>
    public static event Action<float> OnSpeedChanged;

    [Header(" Settings ")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float dashSpeed = 8f;

    Vector2 moveDirection = Vector2.zero;
    float currentSpeed;
    InputAction moveAction;
    InputAction sprintAction;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // 取得
        moveAction = InputManager.Controls.Player.Move;
        sprintAction = InputManager.Controls.Player.Sprint;

        // 処理登録
        moveAction.started += UpdateMoveDirection;
        moveAction.performed += UpdateMoveDirection;
        moveAction.canceled += UpdateMoveDirection;

        sprintAction.started += OnDash;
        sprintAction.canceled += OnWalk;

        // 初期化
        currentSpeed = walkSpeed;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // 処理解除
        moveAction.started -= UpdateMoveDirection;
        moveAction.performed -= UpdateMoveDirection;
        moveAction.canceled -= UpdateMoveDirection;

        sprintAction.started -= OnDash;
        sprintAction.canceled -= OnWalk;

    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        // 移動処理を実行
        Move(moveDirection);

        // Animationの関係上...
        OnSpeedChanged?.Invoke(currentSpeed * moveDirection.magnitude);
    }

    /// <summary>
    /// 移動速度を歩き時に変更する
    /// </summary>
    void OnWalk(InputAction.CallbackContext context)
    {
        currentSpeed = walkSpeed;
    }

    /// <summary>
    /// 移動速度をダッシュ時に変更する
    /// </summary>
    void OnDash(InputAction.CallbackContext context)
    {
        currentSpeed = dashSpeed;
    }

    /// <summary>
    /// 入力に基づいて移動方向更新
    /// </summary>
    /// <param name="context">入力のコンテキスト</param>
    void UpdateMoveDirection(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
        

    /// <summary>
    /// プレイヤーを指定された方向と速度で移動させる
    /// </summary>
    /// <param name="direction">移動方向</param>
    void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        // 計算 (※2軸入力なため、[Y軸(入力) = Z軸(座標移動)]として扱っている！)
        Vector3 movement = ((transform.forward * direction.y)
                            + (transform.right * direction.x)).normalized * currentSpeed * Time.fixedDeltaTime;

        // 座標更新
        transform.position += movement;
    }
}