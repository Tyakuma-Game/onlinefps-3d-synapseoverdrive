using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.SceneManagement;

// TODO: 同じく大規模に作り直す

/// <summary>
/// ゲーム中に発生したイベント処理クラス
/// </summary>
public class EventManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>
    /// コンポーネントがオンになると実行
    /// </summary>
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);//追加
    }


    /// <summary>
    /// コンポーネントがオフになると実行
    /// </summary>
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);//削除
    }


    /// <summary>
    /// ローカルプレイヤーのルーム退出時に実行
    /// </summary>
    public override void OnLeftRoom()
    {
        //基底クラス実装呼び出し
        base.OnLeftRoom();

        //タイトルシーンを読み込む
        SceneManager.LoadScene(0);
    }


    /// <summary>
    /// イベント発生時に呼び出される
    /// </summary>
    /// <param name="photonEvent">そのイベントデータ</param>
    public void OnEvent(EventData photonEvent)
    {
        //自作のイベントか判定
        if (photonEvent.Code < 200) //※200以上はphoton独自のイベント
        {
            //イベントコードを格納（型変換）
            EventCodes eventCode = (EventCodes)photonEvent.Code;

            //イベントのカスタムデータにアクセス
            object[] data = (object[])photonEvent.CustomData;

            //対応処理を実行
            switch (eventCode)
            {
                case EventCodes.NewPlayer:  //マスターが新規ユーザー情報処理する
                    GameManager.instance.NewPlayerSet(data);
                    break;

                case EventCodes.ListPlayers://ユーザー情報を共有
                    GameManager.instance.ListPlayersSet(data);
                    break;

                case EventCodes.UpdateStat: //キルデス数の更新
                    GameManager.instance.ScoreSet(data);
                    break;
            }
        }
    }
}