using UnityEngine;

/// <summary>
/// キーボード入力のインタフェース
/// </summary>
public interface IKeyBoardInput
{
    /// <summary>
    /// WASDキー＋矢印キーの入力を取得
    /// </summary>
    /// <returns>WASDキー＋矢印キーの入力</returns>
    Vector3 GetWASDAndArrowKeyInput();

    /// <summary>
    /// ジャンプキーの入力状態取得
    /// </summary>
    /// <returns>ジャンプキーの入力状態</returns>
    bool GetJumpKeyInput();

    /// <summary>
    /// ダッシュキーの入力状態取得
    /// </summary>
    /// <returns>ダッシュキーの入力状態</returns>
    bool GetRunKeyInput();

    /// <summary>
    /// カーソルロックキーの入力状態取得
    /// </summary>
    /// <returns>カーソルロックキーの入力状態</returns>
    bool GetCursorLockKeyInput();
}