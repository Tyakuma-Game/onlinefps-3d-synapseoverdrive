using UnityEngine;

/// <summary>
/// プレイヤー視点のズームに関するインターフェース
/// </summary>
public interface IPlayerViewPointZoom
{
    /// <summary>
    /// 開始地点から徐々にズームする
    /// </summary>
    /// <param name="camera">対象のカメラ</param>
    /// <param name="adsZoom">ズーム倍率</param>
    /// <param name="adsSpeed">ズーム速度</param>
    void GunZoomIn(Camera camera, float adsZoom, float adsSpeed);

    /// <summary>
    /// 元の地点に徐々に戻す
    /// </summary>
    /// <param name="camera">対象のカメラ</param>
    /// <param name="adsZoom">ズーム前カメラ倍率</param>
    /// <param name="adsSpeed">ズーム速度</param>
    void GunZoomOut(Camera camera, float CameraBaseFactor, float adsSpeed);
}