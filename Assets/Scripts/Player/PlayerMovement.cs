using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Playerの移動に関するクラス
/// </summary>
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [Header("移動関連")]

    [Tooltip("Playerの歩き速度")]
    [SerializeField] float WALK_SPEED = 4f;

    [Tooltip("Playerの走る速度")]
    [SerializeField] float RUN_SPEED = 8f;

    Vector3 moveDir;            //プレイヤーの入力格納（移動）
    Vector3 movement;           //進む方向の格納変数
    float activeMoveSpeed = 4;  //現在の移動速度


    [Header("ジャンプ関連")]

    [Tooltip("Playerのジャンプ力")]
    [SerializeField] Vector3 JUMP_FORCE = new Vector3(0, 6, 0);

    [Tooltip("地面だと認識するレイヤー")]
    [SerializeField] LayerMask groundLayers;

    [Tooltip("ジャンプ中なのか判定フラグ")]
    bool isJumping = false;

    Rigidbody rb;
    

    void Awake()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            return;//処理終了
        }
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            return;//処理終了
        }

        //移動関数
        PlayerMove();

        //ジャンプしていないなら
        if (!isJumping)
        {
            //走りの関数を呼ぶ
            PlayerRun();

            //ジャンプ関数を呼ぶ
            PlayerJump();
        }
    }

    /// <summary>
    /// Playerの移動
    /// </summary>
    public void PlayerMove()
    {
        //入力を格納（wasdや矢印の入力）
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),
            0, Input.GetAxisRaw("Vertical"));

        //ベクトルを計算して正規化
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized;

        //移動量を計算して移動
        transform.position += movement * activeMoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// オブジェクト接触時
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        //ジャンプ中かつ地面レイヤーのオブジェクトとの接触なら
        if (isJumping && (groundLayers.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            //ジャンプを解除
            isJumping = false;
        }
    }

    /// <summary>
    /// Playerのジャンプ処理
    /// </summary>
    public void PlayerJump()
    {
        //ジャンプキーが押されているか判定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ジャンプフラグを立てる
            isJumping = true;

            //瞬間的に真上に力を加える
            rb.AddForce(JUMP_FORCE, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Playerの走り処理
    /// </summary>
    public void PlayerRun()
    {
        //ダッシュ中の移動速度切り替え
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = RUN_SPEED;
        }
        else
        {
            activeMoveSpeed = WALK_SPEED;
        }
    }

    /// <summary>
    /// 現在の移動値を取得
    /// </summary>
    public Vector3 GetMoveDir()
    {
        return this.moveDir;
    }
}