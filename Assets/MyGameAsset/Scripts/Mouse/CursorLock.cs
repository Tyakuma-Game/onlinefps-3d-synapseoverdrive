using UnityEngine;
using UnityEngine.InputSystem;

// TODO: 将来的に仮想マウスも実装する

/// <summary>
/// マウスのロック状態を管理するクラス
/// </summary>
public class CursorLock : MonoBehaviour
{
    InputAction lockAction;

    void Start()
    {
        // 非表示に設定
        LockCursor();

        // 取得
        lockAction = InputManager.Controls.Mouse.Cursorlock;

        // 処理登録
        lockAction.performed += ToggleCursorLockState;
    }

    void OnDestroy()
    {
        // 表示に設定
        UnlockCursor();

        // 処理解除
        lockAction.performed -= ToggleCursorLockState;
    }

    /// <summary>
    /// マウスをロックしカーソル非表示
    /// </summary>
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// マウスのロックを解除しカーソル表示
    /// </summary>
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// マウスのロック状態を切り替え
    /// </summary>
    void ToggleCursorLockState(InputAction.CallbackContext context)
    {
        // マウスのロック状態を切り替え
        if (Cursor.lockState == CursorLockMode.Locked)
            UnlockCursor();     // マウスロック解除
        else
            LockCursor();       // マウスロック
    }
}