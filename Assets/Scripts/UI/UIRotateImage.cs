using UnityEngine;

public class UIRotateImage : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // 回転速度（度/秒）

    private void Update()
    {
        // フレーム毎に回転させる角度を計算
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // Z軸を中心にImageを回転させる
        transform.Rotate(Vector3.forward, rotationAngle);
    }
}
