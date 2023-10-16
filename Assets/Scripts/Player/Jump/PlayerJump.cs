using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerのジャンプに関するクラス
/// </summary>
public class PlayerJump : MonoBehaviour, IPlayerJump
{
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    public void Jump(Vector3 jumpForth)
    {
        // ジャンプ処理
        myRigidbody.AddForce(jumpForth, ForceMode.Impulse);
    }
}