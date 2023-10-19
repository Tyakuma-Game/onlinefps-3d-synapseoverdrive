using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの視点処理をまとめて管理するクラス
/// </summary>
public class PlayerViewPointController : MonoBehaviour
{
    PlayerViewPointRotation playerViewPointRotation;
    PlayerViewPointRay playerViewPointRay;

    PlayerViewPointController()
    {
        playerViewPointRotation = new PlayerViewPointRotation();
        playerViewPointRay = new PlayerViewPointRay();
    }

    /// <summary>
    /// Playerの視点回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    /// <param name="viewPoint">視点座標</param>
    /// <param name="rotationRange">回転範囲</param>
    /// <return>新しい視点座標</return>
    public Transform Rotation(Vector2 rotaInput, Transform viewPoint, float rotationRange)
    {
        return playerViewPointRotation.Rotation(rotaInput, viewPoint, rotationRange);
    }

    /// <summary>
    /// カメラの中央からRayを生成
    /// </summary>
    /// <param name="camera">レイを発射するカメラ</param>
    public Ray GenerateRayFromCameraCenter(Camera camera)
    {
        return playerViewPointRay.GenerateRay(camera,new Vector2(.5f, .5f));
    }

    // Effect

}