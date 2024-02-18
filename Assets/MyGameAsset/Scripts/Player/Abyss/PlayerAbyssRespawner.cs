using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

/// <summary>
/// プレイヤーの奈落判定管理クラス
/// </summary>
public class PlayerAbyssRespawner : MonoBehaviourPunCallbacks
{
    [Header("定数")]
    [Tooltip("奈落判定を行う高さ")]
    [SerializeField] float PITFALL_COORDINATE = -25f;
    bool isRespawns = false;

    private void Awake()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }
    }


    void Update()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //下限突破しているなら
        if (transform.position.y <= PITFALL_COORDINATE && isRespawns == false)
        {
            isRespawns = true;
            AbyssRespawn();
        }
    }


    /// <summary>
    /// Playerの奈落処理
    /// </summary>
    public void AbyssRespawn()
    {
        //死亡関数を呼び出し
        SpawnManager.instance.Die();

        //死亡UIを更新
        UIManager.instance.UpdateDeathUI();
    }
}