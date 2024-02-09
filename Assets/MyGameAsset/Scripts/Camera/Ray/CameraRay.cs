using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラのRay生成を行うクラス
/// </summary>
public class CameraRay : MonoBehaviour,ICameraRay
{
    /// <summary>
    /// カメラから場所を指定してRayを生成
    /// </summary>
    /// <param name="camera">生成するカメラ</param>
    /// <param name="generationPos">生成する座標</param>
    /// <returns>生成したRay</returns>
    public Ray GenerateRay(Camera camera, Vector2 generationPos)
    {
        return camera.ViewportPointToRay(generationPos);
    }
}