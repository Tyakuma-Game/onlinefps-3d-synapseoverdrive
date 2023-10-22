using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Playerの視点移動に関するクラス
/// </summary>
public class PlayerViewpointShift : MonoBehaviourPunCallbacks
{
    [Header("Player視点移動関連")]

    [Tooltip("カメラ")]
    [SerializeField] Camera cam;

    [Tooltip("カメラの元の絞り倍率")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    void Start()
    {
        //自分以外なら
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //メインカメラ格納
        cam = Camera.main;
    }

    /// <summary>
    /// 開始地点から徐々にズームする
    /// </summary>
    /// <param name="adsZoom">覗き込み時のズーム</param>
    /// <param name="adsSpeed">覗き込みの速度</param>
    public void GunZoomIn(float adsZoom, float adsSpeed)
    {
        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,            //開始地点
            adsZoom,                    //目的地点
            adsSpeed * Time.deltaTime); //補完数値
    }

    /// <summary>
    /// 元の地点に徐々に戻す
    /// </summary>
    /// <param name="adsSpeed">覗き込みの速度</param>
    public void GunZoomOut(float adsSpeed)
    {
        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,            //開始地点
            CAMERA_APERTURE_BASE_FACTOR,//目的地点
            adsSpeed * Time.deltaTime); //補完数値
    }

    /// <summary>
    /// カメラの中央からRayを生成
    /// </summary>
    public Ray GenerateRayFromCameraCenter()
    {
        return cam.ViewportPointToRay(new Vector2(.5f, .5f));
    }
}