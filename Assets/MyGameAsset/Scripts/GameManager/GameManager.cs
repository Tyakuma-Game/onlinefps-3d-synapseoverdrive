using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

// 全体的にリファクタリングする
// 同じく分割する

/// <summary>
/// ゲームの状態管理クラス
/// </summary>
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance { get; private set; }

    [Header("定数")]
    [Tooltip("クリアするまでのキル数")]
    [SerializeField] int GAMECLEAR_KILL_SCORE = 3;

    [Tooltip("終了してからの待機時間")]
    [SerializeField] float WAIT_ENDING_TIME = 5f;


    [Header("参照")]
    [Tooltip("プレイヤー情報を扱うクラスのリスト")]
    [SerializeField] List<PlayerInfo> playerList = new List<PlayerInfo>();

    [Tooltip("ゲームの状態を格納")]
    [SerializeField] GameState state;


    [Tooltip("playerinfoのリスト")]
    List<PlayerInformation> playerInfoList = new List<PlayerInformation>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }


    void Start()
    {
        //ネットワーク接続されていない場合
        if (!PhotonNetwork.IsConnected)
        {
            //タイトルに戻る
            SceneManager.LoadScene(0);
        }
        else//繋がっている場合
        {
            //マスターにユーザー情報を発信
            NewPlayerGet(PhotonNetwork.NickName);

            //状態をゲーム中に設定する
            state = GameState.Playing;
        }
    }

    void Update()
    {
        //タブボタンが押されたとき
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //更新しつつ開く
            ShowScoreboard();
        }
        //タブボタンが離された時
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            UIManager.instance.ChangeScoreUI();
        }

        //Homeが押されたとき
        if (Input.GetKeyDown(KeyCode.Home))
        {
            UIManager.instance.ChangeReturnToTitlePanel();
        }
        //タブボタンが離された時
        else if (Input.GetKeyUp(KeyCode.Home))
        {
            UIManager.instance.ChangeReturnToTitlePanel();
        }
    }


    /// <summary>
    ///  新規ユーザーがネットワーク経由でマスターに自分の情報を送信
    /// </summary>
    public void NewPlayerGet(string name)//イベント発生させる関数
    {
        //変数宣言
        object[] info = new object[4];                  //データ格納配列を作成

        //格納
        info[0] = name;                                 //Player名前
        info[1] = PhotonNetwork.LocalPlayer.ActorNumber;//ユーザーの管理番号
        info[2] = 0;                                    //キル数
        info[3] = 0;                                    //デス数

        // RaiseEventでカスタムイベントを発生：データ送信
        PhotonNetwork.RaiseEvent((byte)EventCodes.NewPlayer,                    //発生させるイベント
            info,                                                               //送信データ         （プレイヤー）
            new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient },   //送信相手設定       （ルームマスターのみ）
            new SendOptions { Reliability = true }                              //信頼性の設定       （信頼できるのでプレイヤーに送信される）
        );
    }


    /// <summary>
    /// 送られてきた新プレイヤーの情報をリストに格納
    /// </summary>
    public void NewPlayerSet(object[] data)//マスターが行う処理　イベント発生時に行う処理
    {
        //ネットワークからプレイヤー情報を取得
        PlayerInfo player = new PlayerInfo((string)data[0], (int)data[1], (int)data[2], (int)data[3]);

        //リストに追加
        playerList.Add(player);

        //他のプレイヤーに共有
        ListPlayersGet();
    }


    /// <summary>
    /// 取得したプレイヤー情報をルーム内の全プレイヤーに送信
    /// </summary>
    public void ListPlayersGet()//マスターが行う処理　イベントを発生させる関数
    {
        //ゲームの状況＆プレイヤー情報格納配列作成
        object[] info = new object[playerList.Count + 1];

        //ゲームの状況を格納
        info[0] = state;

        //参加ユーザーの数分ループ
        for (int i = 0; i < playerList.Count; i++)
        {
            //一時的格納する配列
            object[] temp = new object[4];

            //仮格納
            temp[0] = playerList[i].name;
            temp[1] = playerList[i].actor;
            temp[2] = playerList[i].kills;
            temp[3] = playerList[i].deaths;

            //プレイヤー情報を格納している配列に格納 (0にはゲームの状態が入っているため＋１)
            info[i + 1] = temp;
        }

        // RaiseEventでカスタムイベントを発生：データ送信
        PhotonNetwork.RaiseEvent((byte)EventCodes.ListPlayers,          //発生させるイベント
            info,                                                       //送信データ         （プレイヤー）
            new RaiseEventOptions { Receivers = ReceiverGroup.All },    //送信相手設定       （全員）
            new SendOptions { Reliability = true }                      //信頼性の設定       （信頼できるのでプレイヤーに送信される）
        );
    }


    /// <summary>
    /// ListPlayersSendで新しくプレイヤー情報が送られてきたので、リストに格納
    /// </summary>
    public void ListPlayersSet(object[] data)//イベントが発生したら呼ばれる関数　全プレイヤーで呼ばれる
    {
        //既に持っているプレイヤーのリストを初期化
        playerList.Clear();

        //ゲーム状態を変数に格納
        state = (GameState)data[0];

        //1にする 0はゲーム状態なので1から始める
        for (int i = 1; i < data.Length; i++)
        {
            object[] info = (object[])data[i];

            PlayerInfo player = new PlayerInfo(
                (string)info[0],    //名前
                (int)info[1],       //管理番号
                (int)info[2],       //キル
                (int)info[3]);      //デス

            //リストに追加
            playerList.Add(player);
        }

        //ゲームの状態判定
        StateCheck();
    }


    /// <summary>
    /// キル数やデス数の取得関数
    /// </summary>
    /// <param name="actor">プレイヤー識別番号</param>
    /// <param name="stat">キルかデスを数値で判定</param>
    /// <param name="amount">加算する数値</param>
    public void ScoreGet(int actor, int stat, int amount)
    {
        object[] package = new object[] { actor, stat, amount };

        //データを送るイベント
        PhotonNetwork.RaiseEvent((byte)EventCodes.UpdateStat,           //発生させるイベント
            package,                                                    //送信データ         （プレイヤーのキルデス）
            new RaiseEventOptions { Receivers = ReceiverGroup.All },    //送信相手設定       （全員）
            new SendOptions { Reliability = true }                      //信頼性の設定       （信頼できるのでプレイヤーに送信される)
        );
    }


    /// <summary>
    /// 受け取ったデータからリストにキルデス情報を追加
    /// </summary>
    public void ScoreSet(object[] data)
    {
        //変数
        int actor = (int)data[0];   //識別数
        int stat = (int)data[1];    //キルなのかデスなのか
        int amount = (int)data[2];  //加算する数値

        //プレイヤーの数分ループ
        for (int i = 0; i < playerList.Count; i++)
        {
            //情報を取得したプレイヤーと数値が合致したとき
            if (playerList[i].actor == actor)
            {
                switch (stat)
                {
                    case 0://キル
                        playerList[i].kills += amount;
                        break;

                    case 1://デス
                        playerList[i].deaths += amount;
                        break;
                }

                //処理完了
                break;
            }
        }

        //スコア確認
        TargetScoreCheck();
    }


    /// <summary>
    /// スコアボード表示
    /// </summary>
    void ShowScoreboard()
    {
        //スコアUIを開く
        UIManager.instance.ChangeScoreUI();

        //リストの数分ループ
        foreach (PlayerInformation info in playerInfoList)
        {
            //スコアボードに表示されている全員の戦績を削除
            Destroy(info.gameObject);
        }

        //リストから削除
        playerInfoList.Clear();

        //ゲームに参加しているプレイヤーの数分ループ
        foreach (PlayerInfo player in playerList)
        {
            //プレイヤー情報を表示するオブジェクトを生成
            PlayerInformation newPlayerDisplay = Instantiate(UIManager.instance.GetPlayerInformation(), UIManager.instance.GetPlayerInformation().transform.parent);

            //生成したオブジェクトに戦績を反映
            newPlayerDisplay.SetPlayerDetailes(player.name, player.kills, player.deaths);

            //表示
            newPlayerDisplay.gameObject.SetActive(true);

            //リストに追加
            playerInfoList.Add(newPlayerDisplay);
        }
    }


    /// <summary>
    /// ゲームクリア条件を達成したか確認
    /// </summary>
    void TargetScoreCheck()
    {
        //ゲームクリア判定フラグ
        bool clearFlg = false;

        //人数分ループ
        foreach (PlayerInfo player in playerList)
        {
            //条件判定
            if (player.kills >= GAMECLEAR_KILL_SCORE && GAMECLEAR_KILL_SCORE > 0)
            {
                clearFlg = true;   //クリア判定
                break;             //処理終了
            }
        }

        //クリア判定
        if (clearFlg)
        {
            if (PhotonNetwork.IsMasterClient && state != GameState.Ending)
            {
                state = GameState.Ending;   //状態変更
                ListPlayersGet();           //最終的なプレイヤー情報を更新
            }
        }
    }


    /// <summary>
    /// ゲームの状態次第でゲーム終了
    /// </summary>
    void StateCheck()
    {
        //状態の判定
        if (state == GameState.Ending)
        {
            //終了関数を呼ぶ
            EndGame();
        }
    }


    /// <summary>
    /// ゲーム終了関数
    /// </summary>
    void EndGame()
    {
        //マスターなら
        if (PhotonNetwork.IsMasterClient)
        {
            //ネットワーク上から削除
            PhotonNetwork.DestroyAll();
        }

        //ゲーム終了パネル表示
        UIManager.instance.OpenEndPanel();

        //スコア表示
        ShowScoreboard();

        //カーソル表示        
        Cursor.lockState = CursorLockMode.None;

        //一定時間後終了処理を実行
        Invoke("ProcessingAfterCompletion", WAIT_ENDING_TIME);
    }


    /// <summary>
    /// 終了後の処理
    /// </summary>
    private void ProcessingAfterCompletion()
    {
        PhotonNetwork.AutomaticallySyncScene = false;   //シーンの同期設定を切断
        PhotonNetwork.LeaveRoom();                      //部屋を退出
    }


    /// <summary>
    /// プレイヤー情報管理リストから該当プレイヤー情報削除
    /// </summary>
    public void OutPlayerGet(int actor)
    {
        //参加人数分実行
        for (int i = 0; i < playerList.Count; i++)
        {
            //情報を取得したユーザーと数値が合致したとき
            if (playerList[i].actor == actor)
            {
                //抜けたユーザーの情報だけ削除
                playerList.RemoveAt(i);

                //処理完了
                break;
            }
        }

        //プレイヤー情報を更新
        ListPlayersGet();
    }
}