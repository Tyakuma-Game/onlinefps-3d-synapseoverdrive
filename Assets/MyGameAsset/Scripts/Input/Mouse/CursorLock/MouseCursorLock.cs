using Photon.Pun;
using UnityEngine;

/// <summary>
/// マウスのロック状態を管理するクラス
/// </summary>
public class MouseCursorLock : MonoBehaviourPunCallbacks, IMouseCursorLock
{
    void Start()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        LockScreen();
    }

    void OnDestroy()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

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