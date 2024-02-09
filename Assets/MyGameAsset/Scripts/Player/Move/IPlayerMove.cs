using UnityEngine;

/// <summary>
/// Playerの移動に関するインターフェース
/// </summary>
public interface IPlayerMove
{
    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="rigidbody">物理計算用</param>
    void Init(Rigidbody rigidbody);

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="direction">移動のための入力情報</param>
    /// <param name="moveSpeed">移動速度</param>
    void Move(Vector3 direction,float moveSpeed);
}