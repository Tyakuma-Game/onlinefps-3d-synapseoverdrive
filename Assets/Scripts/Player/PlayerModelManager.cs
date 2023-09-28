using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// プレイヤーのモデルに関する処理を行うクラス
/// </summary>
public class PlayerModelManager : MonoBehaviourPunCallbacks
{
    [Header("プレイヤーのモデル")]
    [Tooltip("プレイヤーモデルを格納")]
    [SerializeField] GameObject[] playerModel;

    void Start()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //モデル全て
        foreach (var model in playerModel)
        {
            model.SetActive(false);//非表示
        }
    }
}