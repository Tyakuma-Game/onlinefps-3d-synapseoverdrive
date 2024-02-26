using Photon.Pun;
using System;
using UnityEngine;

/// <summary>
/// 作り直す！
/// </summary>
public class CameraRay : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Rayを含むカスタムイベント引数
    /// </summary>
    public class RayEventArgs : EventArgs
    {
        public Ray Ray { get; set; }
    }

    /// <summary>
    /// Ray生成イベント
    /// </summary>
    public event EventHandler<RayEventArgs> OnRayCreated;

    Camera myCamera;

    void Start()
    {
        myCamera = Camera.main;
    }

    public void CreateRay(Vector2 screenPoint)
    {
        Ray ray = myCamera.ScreenPointToRay(screenPoint);

        // イベント引数にRayをセットしてイベントを発火
        OnRayCreated?.Invoke(this, new RayEventArgs { Ray = ray });
    }
}
