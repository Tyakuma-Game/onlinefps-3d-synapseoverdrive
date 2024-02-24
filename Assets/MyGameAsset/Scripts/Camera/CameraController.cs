using UnityEngine;

// TODO: これもEvent駆動型に変更して効率化する！

/// <summary>
/// カメラに関する処理をまとめて管理するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("カメラの位置オブジェクト")]
    [SerializeField] Transform viewPoint;
    Camera myCamera;

    void Start()
    {
        // 取得
        myCamera = Camera.main;
    }

    void Update()
    { 
        // 位置更新
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
    }

    //−−−−−−−−−−−−−−−−−−−−−/
    // ズーム関連
    //−−−−−−−−−−−−−−−−−−−−−/

    //[Tooltip("カメラの元の絞り倍率")]
    //[SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    /// <summary>
    /// カメラのズームを調整する
    /// </summary>
    /// <param name="targetZoom">目標のズーム倍率</param>
    /// <param name="zoomSpeed">ズーム速度</param>
    public void AdjustCameraZoom(float targetZoom, float zoomSpeed)
    {
        myCamera.fieldOfView = Mathf.Lerp(
            myCamera.fieldOfView,      //開始地点
            targetZoom,                //目的地点
            zoomSpeed * Time.deltaTime //補完数値
        );
    }

    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    //　Ray生成
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

    /// <summary>
    /// カメラから場所を指定してRayを生成
    /// </summary>
    /// <param name="generationPos">生成する座標</param>
    /// <returns>生成したRay</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return myCamera.ViewportPointToRay(generationPos);
    }
}