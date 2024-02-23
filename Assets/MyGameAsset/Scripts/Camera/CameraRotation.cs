using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

/// <summary>
/// カメラの回転を管理するクラス
/// </summary>
public class CameraRotation : MonoBehaviourPunCallbacks
{
    [Header(" Settings ")]
    [SerializeField] float verticalRotationRange = 60f;
    [SerializeField] float sensitivity = 1f;

    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;

    float verticalMouseInput;
    Vector2 rotationInput = Vector2.zero;
    InputAction lookAction;


    void Start()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        lookAction = InputManager.Controls.Player.Look;

        // メソッドをイベントに登録
        lookAction.started += OnLookPerformed;
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
    }

    void OnDestroy()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // イベントからメソッドを削除
        lookAction.started += OnLookPerformed;
        lookAction.performed -= OnLookPerformed;
        lookAction.canceled -= OnLookCanceled;
    }

    void FixedUpdate()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        if (rotationInput == Vector2.zero)
            return;

        // 固定フレームレートで回転処理を実行
        Rotate();
    }

    /// <summary>
    /// 入力が行われた時に呼ばれる処理
    /// </summary>
    void OnLookPerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>(); // 入力値更新
        Debug.Log("視点回転の入力を受け付けました！回転量："+ rotationInput);
    }

    /// <summary>
    /// 入力キャンセル時に呼び出す処理
    /// </summary>
    /// <param name="context"></param>
    void OnLookCanceled(InputAction.CallbackContext context)
    {
        rotationInput = Vector2.zero; // 入力値リセット
        Debug.Log("視点回転の入力が終わりました"+ rotationInput);
    }

    /// <summary>
    /// 視点回転
    /// </summary>
    void Rotate()
    {
        // 回転計算
        verticalMouseInput += rotationInput.y * sensitivity;
        verticalMouseInput = Mathf.Clamp(verticalMouseInput,
                                -verticalRotationRange, verticalRotationRange);

        // 縦の視点回転を反映
        viewPoint.rotation = Quaternion.Euler(-verticalMouseInput, // -を付けないと上下反転
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
    }
}