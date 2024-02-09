using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスのロック状態を管理するクラス
/// </summary>
public class MouseCursorLock : MonoBehaviour, IMouseCursorLock
{
    void Start()
    {
        LockScreen();
    }

    void OnDestroy()
    {
        UnlockScreen();
    }

    /// <summary>
    /// ロック状態に変化
    /// </summary>
    public void LockScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// ロック状態を解除
    /// </summary>
    public void UnlockScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// 現在のロック状態を取得
    /// </summary>
    /// <returns>現在のロック状態</returns>
    public bool IsLocked()
    {
        return Cursor.visible;
    }
}