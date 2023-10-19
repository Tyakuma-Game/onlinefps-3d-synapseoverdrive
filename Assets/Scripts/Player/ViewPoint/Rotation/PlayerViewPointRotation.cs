using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// プレイヤーの視点回転に関するクラス
/// </summary>
public class PlayerViewPointRotation : MonoBehaviour, IPlayerViewPointRotation
{
    // y軸の回転を格納　回転制御用
    float verticalMouseInput;

    /// <summary>
    /// Playerの視点回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    /// <param name="viewPoint">視点座標</param>
    /// <param name="rotationRange">回転範囲</param>
    public Transform Rotation(Vector2 rotaInput, Transform viewPoint, float rotationRange)
    {
        //変数にy軸のマウス入力分の数値を足す
        verticalMouseInput += rotaInput.y;

        //変数の数値を丸める（上下の視点範囲制御）
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -rotationRange, rotationRange);

        //縦の視点回転を反映
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-を付けないと上下反転
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);

        return viewPoint;
    }
}