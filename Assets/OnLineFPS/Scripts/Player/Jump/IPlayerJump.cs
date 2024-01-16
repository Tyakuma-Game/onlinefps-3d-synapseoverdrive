using UnityEngine;

/// <summary>
/// Playerのジャンプインターフェース
/// </summary>
public interface IPlayerJump
{
    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="rigidbody"></param>
    void Init(Rigidbody rigidbody);
    
    /// <summary>
    /// ジャンプ処理
    /// </summary>
    /// <param name="jumpForth">ジャンプ力</param>
    void Jump(Vector3 jumpForth);
}