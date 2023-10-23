using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// カメラに関する処理をまとめて管理するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("カメラの元の絞り倍率")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    // 操作するカメラオブジェクト
    Camera myCamera;


    ICameraZoom cameraZoom;
    ICameraRay cameraRay;

    [SerializeField] CurveControlledBob curveControlledBob;

    void Start()
    {
        // カメラ格納
        myCamera = Camera.main;

        // 処理取得
        cameraZoom = GetComponent<ICameraZoom>();
        cameraRay = GetComponent<ICameraRay>();
    }


    public void UpdatePosition(Transform viewPoint,float moveSpeed)
    {

        Vector3 cameraPositionOffset = CurveControlledBobDoHeadBob(moveSpeed);
        myCamera.transform.localPosition = cameraPositionOffset;

        // カメラ位置更新
        //myCamera.transform.position = viewPoint.position;//座標
        //myCamera.transform.rotation = viewPoint.rotation;//回転
    }

    /// <summary>
    /// 視点の揺れセットアップ
    /// </summary>
    /// <param name="bobBaseInterval">ボブの基本間隔</param>
    public void CurveControlledBobSetUp(float bobBaseInterval)
    {
        curveControlledBob.Setup(myCamera, bobBaseInterval);
    }

    /// <summary>
    /// 視点の揺れを行う
    /// </summary>
    /// <param name="speed">揺れの速度</param>
    /// <returns></returns>
    public Vector3 CurveControlledBobDoHeadBob(float speed)
    {
        return curveControlledBob.DoHeadBob(speed);
    }

    /// <summary>
    /// 開始地点から徐々にズームする
    /// </summary>
    /// <param name="adsZoom">ズーム倍率</param>
    /// <param name="adsSpeed">ズーム速度</param>
    public void GunZoomIn(float adsZoom,float adsSpeed)
    {
        cameraZoom.GunZoomIn(myCamera,adsZoom,adsSpeed);
    }

    /// <summary>
    /// 元の地点に徐々に戻す
    /// </summary>
    /// <param name="adsSpeed">ズーム速度</param>
    public void GunZoomOut(float adsSpeed)
    {
        cameraZoom.GunZoomOut(myCamera, CAMERA_APERTURE_BASE_FACTOR, adsSpeed);
    }

    /// <summary>
    /// カメラから場所を指定してRayを生成
    /// </summary>
    /// <param name="camera">生成するカメラ</param>
    /// <param name="generationPos">生成する座標</param>
    /// <returns>生成したRay</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return cameraRay.GenerateRay(myCamera, generationPos);
    }
}