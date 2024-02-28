using Photon.Pun;
using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PhotonEventManager : MonoBehaviourPunCallbacks
{
    public static event Action<GameObject> OnPlayerInstantiated;

    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        OnPlayerInstantiated?.Invoke(info.photonView.gameObject);
    }
}