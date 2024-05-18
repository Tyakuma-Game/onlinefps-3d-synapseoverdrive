using UnityEngine;
using System.Collections;

/// <summary>
/// カメラの揺れ演出クラス
/// </summary>
public class CameraShaker : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] float shakeMagnitude = 0.2f;
    [SerializeField] float shakeTime = 0.1f;

    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;
    [SerializeField] Transform sabViewPoint;
    Camera myCamera;
    float shakeCount = 0;


    void Awake()
    {
        // 処理登録
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // 処理解除
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        if (PlayerEvent.OnDamage != null)
            PlayerEvent.OnDamage -= OnShake;  // 処理登録
    }

    /// <summary>
    /// プレイヤーがインスタンス化された際に呼ばれる処理
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // 取得
        myCamera = Camera.main;

        // 処理登録
        PlayerEvent.OnDamage += OnShake;  // 処理登録
    }

    /// <summary>
    /// カメラの揺れ演出
    /// </summary>
    void OnShake()
    {       
        shakeCount = 0;                     // 揺れカウントをリセット
        StartCoroutine(ViewPointShake());   // 揺れコルーチンを開始
    }

    /// <summary>
    /// 実際にカメラを揺らすコルーチン
    /// </summary>
    IEnumerator ViewPointShake()
    {
        while (shakeCount < shakeTime)
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

        // 元の位置に
        viewPoint.transform.position = sabViewPoint.transform.position;
    }
}