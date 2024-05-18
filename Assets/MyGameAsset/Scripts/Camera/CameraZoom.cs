using UnityEngine;
using System.Collections;

/// <summary>
/// カメラのズーム処理クラス
/// </summary>
public class CameraZoom : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] float zoomThreshold = 0.01f;

    Camera myCamera;
    Coroutine zoomCoroutine;

    void Awake()
    {
        // 処理登録
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // 処理解除
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        CameraEvents.OnZoomStateChanged -= HandleZoomChange;
    }

    /// <summary>
    /// プレイヤーがインスタンス化された際に呼ばれる処理
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // 取得
        myCamera = Camera.main;

        // 処理登録
        CameraEvents.OnZoomStateChanged += HandleZoomChange;
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

        // 新ズームコルーチン開始
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
        float deltaZoom = Mathf.Abs(myCamera.fieldOfView - targetZoom);
        while (deltaZoom > zoomThreshold)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        // 微調整
        myCamera.fieldOfView = targetZoom;
    }
}