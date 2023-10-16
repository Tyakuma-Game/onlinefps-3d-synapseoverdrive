using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの移動クラス
/// </summary>
public class PlayerMove : MonoBehaviour, IPlayerMove
{
    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="direction">移動のための入力情報</param>
    /// <param name="moveSpeed">移動速度</param>
    public void Move(Vector3 direction, float moveSpeed)
    {
        // 計算
        Vector3 movement = ((transform.forward * direction.z)
                            + (transform.right * direction.x)).normalized;

        // 移動
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}