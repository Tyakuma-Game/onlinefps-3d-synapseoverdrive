using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerViewPointRotation : MonoBehaviour, IPlayerViewPointRotation
{
    [Tooltip("カメラの位置オブジェクト")]
    [SerializeField] Transform viewPoint;

    [Tooltip("視点の上下回転範囲")]
    [SerializeField] float VERTICAL_ROTATION_RANGE = 60f;
    float verticalMouseInput;   //y軸の回転を格納　回転制御用

    /// <summary>
    /// Playerの視点回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    /// /// <param name="rotaSpeed">回転速度</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed)
    {
        //変数にy軸のマウス入力分の数値を足す
        verticalMouseInput += rotaInput.y;

        //変数の数値を丸める（上下の視点範囲制御）
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -VERTICAL_ROTATION_RANGE, VERTICAL_ROTATION_RANGE);

        //縦の視点回転を反映
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-を付けないと上下反転
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
    }
}