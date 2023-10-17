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

    [Tooltip("カメラの位置オブジェクト")]
    [SerializeField] Transform viewPoint;

    [Tooltip("視点移動の速度")]
    [SerializeField] float MOUSE_SENSITIVITY = 1f;

    [Tooltip("視点の上下回転範囲")]
    [SerializeField] float VERTICAL_ROTATION_RANGE = 60f;

    [Tooltip("カメラの元の絞り倍率")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    Vector2 mouseInput;         //ユーザーのマウス入力を格納
    float verticalMouseInput;   //y軸の回転を格納　回転制御用


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

    void Update()
    {
        //自分以外なら
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //視点移動関数
        PlayerRotate();

        //カメラ座標調整
        cam.transform.position = viewPoint.position;//座標
        cam.transform.rotation = viewPoint.rotation;//回転
    }

    /// <summary>
    /// Playerの横回転と縦の視点移動
    /// </summary>
    public void PlayerRotate()
    {
        //変数にユーザーのマウスの動きを格納
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") * MOUSE_SENSITIVITY,
            Input.GetAxisRaw("Mouse Y") * MOUSE_SENSITIVITY);

        //横回転を反映
        transform.rotation = Quaternion.Euler       //オイラー角としての角度が返される
            (transform.eulerAngles.x,
            transform.eulerAngles.y + mouseInput.x, //マウスのx軸の入力を足す
            transform.eulerAngles.z);



        //変数にy軸のマウス入力分の数値を足す
        verticalMouseInput += mouseInput.y;

        //変数の数値を丸める（上下の視点範囲制御）
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -VERTICAL_ROTATION_RANGE, VERTICAL_ROTATION_RANGE);

        //縦の視点回転を反映
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-を付けないと上下反転
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
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