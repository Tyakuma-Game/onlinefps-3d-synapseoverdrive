using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

/// <summary>
/// ルーム情報クラス
/// </summary>
public class Room : MonoBehaviour
{
    [Header("参照")]
    [Tooltip("ルーム名反映用テキスト")]
    [SerializeField] Text buttonText;

    [Tooltip("部屋情報の格納変数")]
    RoomInfo info;


    /// <summary>
    /// ルームボタンに詳細を登録
    /// </summary>
    public void RegisterRoomDetails(RoomInfo info)
    {
        //ルームのinfoに引数のinfoを格納
        this.info = info;

        //部屋名を更新
        buttonText.text = this.info.Name;
    }


    /// <summary>
    /// このルームボタンが管理しているルームに参加
    /// </summary>
    public void OpenRoom()
    {
        //ルーム参加関数を呼び出し
        PhotonManager.instance.JoinRoom(info);
    }
}