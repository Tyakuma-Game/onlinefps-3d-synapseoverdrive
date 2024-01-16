
/// <summary>
/// マウスのロック状態を管理のインターフェース
/// </summary>
public interface IMouseCursorLock
{
    /// <summary>
    /// ロック状態にする
    /// </summary>
    void LockScreen();

    /// <summary>
    /// ロック状態を解除
    /// </summary>
    void UnlockScreen();

    /// <summary>
    /// 現在のロック状態を取得
    /// </summary>
    /// <returns>現在のロック状態</returns>
    bool IsLocked();
}