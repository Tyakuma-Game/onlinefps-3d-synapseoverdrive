using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キーボードの入力処理を管理するクラス
/// </summary>
public class KeyBoardInput : MonoBehaviour, IKeyBoardInput
{
    /// <summary>
    /// WASDキー＋矢印キーの入力を取得
    /// </summary>
    /// <returns>WASDキー＋矢印キーの入力</returns>
    public Vector3 GetWASDAndArrowKeyInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"),
                           0, Input.GetAxisRaw("Vertical"));
    }

    /// <summary>
    /// ジャンプキーの入力状態取得
    /// </summary>
    /// <returns>ジャンプキーの入力状態</returns>
    public bool GetJumpKeyInput()
    {
        return Input.GetKey(KeyCode.Space);
    }

    /// <summary>
    /// ダッシュキーの入力状態取得
    /// </summary>
    /// <returns>ダッシュキーの入力状態</returns>
    public bool GetRunKeyInput()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}