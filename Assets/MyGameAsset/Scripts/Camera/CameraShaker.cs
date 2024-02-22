using System.Collections;
using UnityEngine;
using Photon.Pun;

public class CameraShaker : MonoBehaviourPunCallbacks
{
    [SerializeField] float shakeMagnitude = 0.2f;
    [SerializeField] float shakeTime = 0.1f;
    [SerializeField] float shakeCount = 0;

    [Tooltip("カメラの位置オブジェクト")]
    [SerializeField] Transform viewPoint;

    [Tooltip("カメラの位置オブジェクトの予備")]
    [SerializeField] Transform sabViewPoint;
    Camera myCamera;

    void Start()
    {
        // 自身でない場合は処理終了
        if (!photonView.IsMine)
            return;

        // カメラ格納
        myCamera = Camera.main;

        // カメラの揺れをダメージEventに追加
        PlayerEvent.onDamage += Shake;
    }

    void OnDestroy()
    {
        // 自身でない場合は処理終了
        if (!photonView.IsMine)
            return;

        // カメラの揺れをEventから削除
        PlayerEvent.onDamage -= Shake;
    }


    void Shake()
    {
        shakeCount = 0;
        StartCoroutine(ViewPointShake());
    }

    IEnumerator ViewPointShake()
    {
        while (shakeCount < shakeTime)
        {
            float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            viewPoint.transform.position = new Vector3(x, y, sabViewPoint.transform.position.z);
            myCamera.transform.position = viewPoint.transform.position;

            shakeCount += Time.deltaTime;

            yield return null;
        }
        viewPoint.transform.position = sabViewPoint.transform.position;
    }
}