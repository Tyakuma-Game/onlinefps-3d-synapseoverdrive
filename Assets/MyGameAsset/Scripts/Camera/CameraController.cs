using UnityEngine;

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
        // カメラ格納
        myCamera = Camera.main;
    }

    /// <summary>
    /// カメラの更新処理
    /// </summary>
    public void UpdatePosition()
    {
        // カメラ位置更新
        myCamera.transform.position = viewPoint.position;//座標
        myCamera.transform.rotation = viewPoint.rotation;//回転
    }

    //−−−−−−−−−−−−−−−−−−−−−/
    // 視点の回転用Program
    //−−−−−−−−−−−−−−−−−−−−−/

    // y軸の回転を格納　回転制御用
    float verticalMouseInput;

    /// <summary>
    /// Playerの視点回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    /// <param name="rotaSpeed">回転速度</param>
    /// <param name="rotationRange">回転範囲</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed, float rotationRange)
    {
        //変数にy軸のマウス入力分の数値を足す
        verticalMouseInput += rotaInput.y * rotaSpeed;

        //変数の数値を丸める（上下の視点範囲制御）
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -rotationRange, rotationRange);

        //縦の視点回転を反映
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-を付けないと上下反転
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
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