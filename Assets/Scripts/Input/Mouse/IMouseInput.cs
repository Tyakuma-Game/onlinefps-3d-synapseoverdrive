using UnityEngine;

/// <summary>
/// マウス入力のインタフェース
/// </summary>
public interface IMouseInput
{
    /// <summary>
    /// マウスの移動を取得
    /// </summary>
    /// <returns>マウスの移動</returns>
    Vector2 GetMouseMove();
}