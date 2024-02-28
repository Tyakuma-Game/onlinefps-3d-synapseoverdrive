using Photon.Pun;
using System;
using UnityEngine;

/// <summary>
/// ネットワーク上でオブジェクトが生成されたかどうかを判定する処理
/// </summary>
public class PhotonEventManager : MonoBehaviour, IPunInstantiateMagicCallback
{
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal)
        {
            Debug.Log("自身がネットワークオブジェクトを生成しました");
        }
        else
        {
            Debug.Log("他プレイヤーがネットワークオブジェクトを生成しました");
        }

        //OnPlayerInstantiated?.Invoke(info.photonView.gameObject);
    }

}