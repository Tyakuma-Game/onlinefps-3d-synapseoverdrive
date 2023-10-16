using UnityEngine;

/// <summary>
/// Playerのジャンプインターフェース
/// </summary>
public interface IPlayerJump
{
    /// <summary>
    /// ジャンプ処理
    /// </summary>
    /// <param name="jumpForth">ジャンプ力</param>
    void Jump(Vector3 jumpForth);
}