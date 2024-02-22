using UnityEngine;
using Photon.Pun;

/// <summary>
/// プレイヤーのモデルに関する処理を行うクラス
/// </summary>
public class PlayerModelManager : MonoBehaviourPunCallbacks
{
    [Header("プレイヤーのモデル")]
    [SerializeField] GameObject[] playerModel;

    void Start()
    {
        // 自分以外の場合は
        if (!photonView.IsMine)
            return; // 処理終了

        // モデルを非表示に
        foreach (var model in playerModel)
            model.SetActive(false);
    }
}