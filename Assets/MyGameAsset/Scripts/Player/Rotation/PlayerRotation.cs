using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの回転クラス
/// </summary>
public class PlayerRotation : MonoBehaviour, IPlayerRotation
{
    /// <summary>
    /// Playerの回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    /// /// <param name="rotaSpeed">回転速度</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed)
    {
        // 計算
        Vector2 rotation = new Vector2(rotaInput.x * rotaSpeed, 0);

        //横回転を反映
        transform.rotation = Quaternion.Euler           //オイラー角としての角度が返される
                (transform.eulerAngles.x,
                transform.eulerAngles.y + rotation.x,   //マウスのx軸の入力を足す
                transform.eulerAngles.z);
    }
}