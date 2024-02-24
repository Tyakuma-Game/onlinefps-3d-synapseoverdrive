using UnityEngine;
using System;
using Photon.Pun;

// TODO: これもEvent駆動型に変更して効率化する！

/// <summary>
/// カメラに関する処理をまとめて管理するクラス
/// </summary>
public class CameraController : MonoBehaviourPunCallbacks
{
    [Tooltip("カメラの位置オブジェクト")]
    [SerializeField] Transform viewPoint;
    Camera myCamera;

    void Start()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // 取得
        myCamera = Camera.main;
    }

    void Update()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // 位置更新
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
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