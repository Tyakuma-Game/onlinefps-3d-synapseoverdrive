using System.Collections;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// カメラの揺れ演出クラス
/// </summary>
public class CameraShaker : MonoBehaviourPunCallbacks
{
    [Header(" Settings ")]
    [SerializeField] float shakeMagnitude = 0.2f;
    [SerializeField] float shakeTime = 0.1f;

    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;
    [SerializeField] Transform sabViewPoint;
    Camera myCamera;
    float shakeCount = 0;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        myCamera = Camera.main;         // メインカメラを取得
        PlayerEvent.onDamage += Shake;  // 処理登録
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // 処理解除
        PlayerEvent.onDamage -= Shake;
    }

    /// <summary>
    /// カメラの揺れ演出
    /// </summary>
    void Shake()
    {
        shakeCount = 0;                     // 揺れカウントをリセット
        StartCoroutine(ViewPointShake());   // 揺れコルーチンを開始
    }

    /// <summary>
    /// 実際にカメラを揺らすコルーチン
    /// </summary>
    IEnumerator ViewPointShake()
    {
        while (shakeCount < shakeTime)// 指定した時間が経過するまでループ
        {
            // 揺れ計算
            float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            viewPoint.transform.position = new Vector3(x, y, sabViewPoint.transform.position.z);
            
            // 更新
            myCamera.transform.position = viewPoint.transform.position;
            shakeCount += Time.deltaTime;
            yield return null;
        }

        // 揺れが終わったらカメラを元の位置に
        viewPoint.transform.position = sabViewPoint.transform.position;
    }
}