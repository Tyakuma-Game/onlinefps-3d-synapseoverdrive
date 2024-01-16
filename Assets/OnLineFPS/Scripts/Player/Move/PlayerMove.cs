using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの移動クラス
/// </summary>
public class PlayerMove : MonoBehaviour, IPlayerMove
{
    Rigidbody myRigidbody;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="rigidbody">物理計算用</param>
    public void Init(Rigidbody rigidbody)
    {
        myRigidbody = rigidbody;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="direction">移動のための入力情報</param>
    /// <param name="moveSpeed">移動速度</param>
    public void Move(Vector3 direction, float moveSpeed)
    {
        // 計算
        Vector3 movement = ((transform.forward * direction.z)
                            + (transform.right * direction.x)).normalized * moveSpeed * Time.deltaTime;

        // TODO: Rigidbodyを使用した座標移動に修正
        // 試にRigidbodyを使用する方法に切り替えたところ自作のPhotonネットワーク上での座標予測処理が上手く動作しなくなった
        // position直入でも処理自体にバグは発生していない為後ほど修正

        // 移動
        transform.position += movement;
    }
}