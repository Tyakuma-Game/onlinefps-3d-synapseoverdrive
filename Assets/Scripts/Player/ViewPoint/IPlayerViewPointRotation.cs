using UnityEngine;

/// <summary>
/// Playerの回転に関するインターフェース
/// </summary>
public interface IPlayerViewPointRotation
{
    /// <summary>
    /// Playerの視点回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    /// /// <param name="rotaSpeed">回転速度</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed);
}