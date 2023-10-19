using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのカメラから発射するレイを管理するクラス
/// </summary>
public class PlayerViewPointRay : MonoBehaviour
{
    /// <summary>
    /// カメラの指定した場所からレイを発射
    /// </summary>
    /// <param name="camera">レイを発射するカメラ</param>
    /// <param name="generationPos">発射する場所</param>
    public Ray GenerateRay(Camera camera, Vector2 generationPos)
    {
        return camera.ViewportPointToRay(generationPos);
    }
}