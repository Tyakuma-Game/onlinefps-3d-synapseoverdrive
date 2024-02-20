using System.Collections;
using UnityEngine;

/// <summary>
/// カメラに関する処理をまとめて管理するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("カメラの元の絞り倍率")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    [Tooltip("カメラの位置オブジェクト")]
    [SerializeField] Transform viewPoint;

    [Tooltip("カメラの位置オブジェクトの予備")]
    [SerializeField] Transform sabViewPoint;

    // 操作するカメラオブジェクト
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
    // Damage時の揺れ処理
    //−−−−−−−−−−−−−−−−−−−−−/

    public void Shake()
    {
        shakeCount = 0;
        StartCoroutine(ViewPointShake());
    }

    float shakeMagnitude = 0.2f;
    float shakeTime = 0.1f;
    float shakeCount = 0;

    IEnumerator ViewPointShake()
    {
        while(shakeCount < shakeTime)
        {
            float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            viewPoint.transform.position = new Vector3(x,y, sabViewPoint.transform.position.z);
            myCamera.transform.position = viewPoint.transform.position;

            shakeCount += Time.deltaTime;

            yield return null;
        }
        viewPoint.transform.position = sabViewPoint.transform.position;
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

    /// <summary>
    /// 開始地点から徐々にズームする
    /// </summary>
    /// <param name="adsZoom">ズーム倍率</param>
    /// <param name="adsSpeed">ズーム速度</param>
    public void GunZoomIn(float adsZoom,float adsSpeed)
    {
        GunZoomIn(myCamera,adsZoom,adsSpeed);
    }

    /// <summary>
    /// 元の地点に徐々に戻す
    /// </summary>
    /// <param name="adsSpeed">ズーム速度</param>
    public void GunZoomOut(float adsSpeed)
    {
        GunZoomOut(myCamera, CAMERA_APERTURE_BASE_FACTOR, adsSpeed);
    }

    /// <summary>
    /// カメラから場所を指定してRayを生成
    /// </summary>
    /// <param name="camera">生成するカメラ</param>
    /// <param name="generationPos">生成する座標</param>
    /// <returns>生成したRay</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return GenerateRay(myCamera, generationPos);
    }

    /// <summary>
    /// カメラから場所を指定してRayを生成
    /// </summary>
    /// <param name="camera">生成するカメラ</param>
    /// <param name="generationPos">生成する座標</param>
    /// <returns>生成したRay</returns>
    public Ray GenerateRay(Camera camera, Vector2 generationPos)
    {
        return camera.ViewportPointToRay(generationPos);
    }


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