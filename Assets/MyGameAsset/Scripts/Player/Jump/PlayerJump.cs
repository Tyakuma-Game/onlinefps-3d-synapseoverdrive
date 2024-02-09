using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerのジャンプに関するクラス
/// </summary>
public class PlayerJump : MonoBehaviour, IPlayerJump
{
    Rigidbody myRigidbody;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="rigidbody"></param>
    public void Init(Rigidbody rigidbody)
    {
        myRigidbody = rigidbody;
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    public void Jump(Vector3 jumpForth)
    {
        // 重複防止策
        if (myRigidbody.velocity.y < jumpForth.y / 2)
        {
            // 力を加える
            myRigidbody.AddForce(jumpForth, ForceMode.VelocityChange);
        }
    }
}