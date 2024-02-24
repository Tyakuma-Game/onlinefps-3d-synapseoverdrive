using Photon.Pun;
using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// カメラのズーム処理クラス
/// </summary>
public class CameraZoom : MonoBehaviourPunCallbacks
{
    [Header(" Settings ")]
    [SerializeField] float zoomThreshold = 0.01f;

    /// <summary>
    /// ズーム状態が変更されたときに発火するイベント
    /// </summary>
    public static Action<float, float> OnZoomStateChanged;
    Camera myCamera;
    Coroutine zoomCoroutine;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // 取得
        myCamera = Camera.main;

        // 処理登録
        OnZoomStateChanged += HandleZoomChange;
    }

    void OnDestroy()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // 処理解除
        OnZoomStateChanged -= HandleZoomChange;
    }

    /// <summary>
    /// ズーム状態変更時に呼び出される処理
    /// ズーム要求に基づいてズームを開始
    /// </summary>
    /// <param name="targetZoom">目標のズーム倍率</param>
    /// <param name="zoomSpeed">ズーム速度</param>
    void HandleZoomChange(float targetZoom, float zoomSpeed)
    {
        // 既存のズームコルーチンがあれば停止
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        // 新しいズームコルーチン開始
        zoomCoroutine = StartCoroutine(AdjustCameraZoom(targetZoom, zoomSpeed));
    }

    /// <summary>
    /// ズームを徐々に調整するコルーチン
    /// 目標のズーム値に到達するまでフィールドオブビューを更新し続ける
    /// </summary>
    /// <param name="targetZoom">目標のズーム倍率</param>
    /// <param name="zoomSpeed">ズーム速度</param>
    /// <returns>コルーチンの実行制御に使用されるIEnumerator</returns>
    IEnumerator AdjustCameraZoom(float targetZoom, float zoomSpeed)
    {
        // myCamera.fieldOfViewがtargetZoomになるまでループ
        while (Mathf.Abs(myCamera.fieldOfView - targetZoom) > zoomThreshold)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        // 最終的に目標値設定
        myCamera.fieldOfView = targetZoom;
    }
}