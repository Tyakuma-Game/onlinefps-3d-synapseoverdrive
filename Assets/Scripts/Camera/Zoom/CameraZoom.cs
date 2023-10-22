using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラのズームに関する処理クラス
/// </summary>
public class CameraZoom : MonoBehaviour, ICameraZoom
{
    /// <summary>
    /// 開始地点から徐々にズームする
    /// </summary>
    /// <param name="camera">対象のカメラ</param>
    /// <param name="adsZoom">ズーム倍率</param>
    /// <param name="adsSpeed">ズーム速度</param>
    public void GunZoomIn(Camera camera, float adsZoom, float adsSpeed)
    {
        camera.fieldOfView = Mathf.Lerp(
            camera.fieldOfView,         //開始地点
            adsZoom,                    //目的地点
            adsSpeed * Time.deltaTime); //補完数値
    }

    /// <summary>
    /// 元の地点に徐々に戻す
    /// </summary>
    /// <param name="camera">対象のカメラ</param>
    /// <param name="adsSpeed">ズーム速度</param>
    public void GunZoomOut(Camera camera, float CameraBaseFactor, float adsSpeed)
    {
        camera.fieldOfView = Mathf.Lerp(
            camera.fieldOfView,         //開始地点
            CameraBaseFactor,           //目的地点
            adsSpeed * Time.deltaTime); //補完数値
    }
}