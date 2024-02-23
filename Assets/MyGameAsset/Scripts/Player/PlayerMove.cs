using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

/// <summary>
/// プレイヤーの移動を管理するクラス
/// </summary>
public class PlayerMove : MonoBehaviourPunCallbacks
{
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float dashSpeed = 8f;
    
    Vector2 moveDirection = Vector2.zero;
    float currentSpeed;
    InputAction moveAction;

    void Start()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // InputActionの参照取得
        moveAction = InputManager.Controls.Player.Move;

        // 入力システムのイベントに移動方向の更新メソッドを登録
        moveAction.started += UpdateMoveDirection;
        moveAction.performed += UpdateMoveDirection;
        moveAction.canceled += UpdateMoveDirection;

        // プレイヤーのEventに追加
        PlayerEvent.onWalk += OnWalk;
        PlayerEvent.onDash += OnDash;

        // 移動速度初期化
        currentSpeed = walkSpeed;
    }

    void OnDestroy()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // 登録したイベントを解除
        moveAction.started -= UpdateMoveDirection;
        moveAction.performed -= UpdateMoveDirection;
        moveAction.canceled -= UpdateMoveDirection;

        // プレイヤーのEventから削除
        PlayerEvent.onWalk -= OnWalk;
        PlayerEvent.onDash -= OnDash;
    }

    void FixedUpdate()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // 移動処理を実行
        Move(moveDirection);
    }

    /// <summary>
    /// 移動速度を歩き時に変更する
    /// </summary>
    void OnWalk() => currentSpeed = walkSpeed;

    /// <summary>
    /// 移動速度をダッシュ時に変更する
    /// </summary>
    void OnDash() => currentSpeed = dashSpeed;

    /// <summary>
    /// 入力に基づいて移動方向を更新する
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
        // 移動方向がゼロの場合は処理スキップ
        if (direction == Vector2.zero)
            return;

        // 移動量計算 (※2軸入力なため、[Y軸(入力) = Z軸(座標移動)]として扱っている！)
        Vector3 movement = ((transform.forward * direction.y)
                            + (transform.right * direction.x)).normalized * currentSpeed * Time.fixedDeltaTime;

        // 計算した移動量でプレイヤーの位置を更新
        transform.position += movement;
    }
}