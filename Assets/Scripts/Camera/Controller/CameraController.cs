using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラに関する処理をまとめて管理するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    Camera myCamera;


    ICameraZoom cameraZoom;

    // 入力システム
    [Tooltip("キーボードの入力処理")]
    IKeyBoardInput keyBoardInput;

    [Tooltip("マウスの入力処理")]
    IMouseInput mouseInput;


    void Start()
    {
        // カメラ格納
        myCamera = Camera.main;

        // 処理取得
        cameraZoom = GetComponent<ICameraZoom>();

        // 入力処理
        keyBoardInput = GetComponent<IKeyBoardInput>();
        mouseInput = GetComponent<IMouseInput>();
    }

    /// <summary>
    /// カメラのズーム処理
    /// </summary>
    /// <param name="adsZoom"></param>
    /// <param name="adsSpeed"></param>
    public void GunZoomIn(float adsZoom,float adsSpeed)
    {
        cameraZoom.GunZoomIn(myCamera,adsZoom,adsSpeed);
    }
}