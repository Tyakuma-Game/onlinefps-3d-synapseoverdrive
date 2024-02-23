using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Playerの回転クラス
/// </summary>
public class PlayerRotation : MonoBehaviourPunCallbacks
{
    [Header("Settings")]
    [SerializeField] float rotationSpeed = 100f;
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

        // イベントからメソッドの登録を解除
        lookAction.started -= OnLookPerformed;
        lookAction.performed -= OnLookPerformed;
        lookAction.canceled -= OnLookCanceled;
    }

    void FixedUpdate()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        if (rotationInput == Vector2.zero) return;

        Rotate(rotationInput);
    }

    void OnLookPerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    void OnLookCanceled(InputAction.CallbackContext context)
    {
        rotationInput = Vector2.zero;
    }

    /// <summary>
    /// Playerの回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    public void Rotate(Vector2 rotaInput)
    {
        // 計算
        Vector2 rotation = new Vector2(rotaInput.x * rotationSpeed, 0);

        //横回転を反映
        transform.rotation = Quaternion.Euler           //オイラー角としての角度が返される
                (transform.eulerAngles.x,
                transform.eulerAngles.y + rotation.x,   //マウスのx軸の入力を足す
                transform.eulerAngles.z);
    }
}