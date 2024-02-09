using UnityEngine;

/// <summary>
/// Playerの視点回転に関するインターフェース
/// </summary>
public interface IPlayerViewPointRotation
{
    /// <summary>
    /// Playerの視点回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    /// <param name="rotaSpeed">回転速度</param>
    /// <param name="viewPoint">視点座標</param>
    /// <param name="rotationRange">回転範囲</param>
    public Transform Rotation(Vector2 rotaInput, float rotaSpeed, Transform viewPoint, float rotationRange);
}