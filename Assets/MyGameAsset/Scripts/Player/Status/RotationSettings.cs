using UnityEngine;

// TODO: 緊急で実装したからこういった形になっています。
// 後ほどしっかりとデータクラスを制作しリファクタリングを行います。

/// <summary>
/// 回転速度の指定オブジェクト
/// </summary>
[CreateAssetMenu(fileName = "RotationSettings", menuName = "MyFPSGameDate/RotationSettings")]
public class RotationSettings : ScriptableObject
{
    public float rotationSpeed = 1f; // デフォルト値は1f
}