using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスの入力処理を管理するクラス
/// </summary>
public class MouseInput : MonoBehaviour, IMouseInput
{
    /// <summary>
    /// マウスの移動を取得
    /// </summary>
    /// <returns>マウスの移動</returns>
    public Vector2 GetMouseMove()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }
}