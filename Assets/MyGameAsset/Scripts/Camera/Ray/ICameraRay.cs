using UnityEngine;

/// <summary>
/// カメラのRay生成を担当するインターフェース
/// </summary>
public interface ICameraRay
{
    /// <summary>
    /// カメラから場所を指定してRayを生成
    /// </summary>
    /// <param name="camera">生成するカメラ</param>
    /// <param name="generationPos">生成する座標</param>
    /// <returns>生成したRay</returns>
    Ray GenerateRay(Camera camera, Vector2 generationPos);
}