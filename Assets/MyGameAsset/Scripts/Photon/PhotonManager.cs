using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// TODO:大規模な修正が必要
// 全体的にスクリプトを分割し、MVCモデル形式で作り直す

public class RoomDataSettingTest
{
    [SerializeField] int RoomMember;
}

/// <summary>
/// Photon管理のクラス
/// </summary>
public class PhotonManager : MonoBehaviourPunCallbacks
{
    [Header("定数")]
    [Tooltip("プレイヤーの最大参加人数")]
    public int ROOM_MEMBER_MAX = 6;

    [Header("参照")]
    [Tooltip("photon管理クラス")]
    public static PhotonManager instance;

    [Header("ロード画面関連")]
    [SerializeField] GameObject loadingPanel; //ロードパネル
    [SerializeField] Text loadingText;        //ロードテキスト
    [SerializeField] GameObject buttons;      //ボタン

    [Header("ルーム作成画面")]
    [SerializeField] GameObject createRoomPanel;  //ルーム作成パネル
    [SerializeField] Text enterRoomName;          //入力されたルーム名テキスト

    [Header("ルーム画面")]
    [SerializeField] GameObject roomPanel;    //ルームパネル
    [SerializeField] Text roomName;           //ルーム名テキスト

    [Header("ルームのエラー画面")]
    [SerializeField] GameObject errorPanel;   //エラーパネル
    [SerializeField] Text errorText;          //エラーテキスト

    [Header("ルームの一覧画面")]
    [SerializeField] GameObject roomListPanel;//ルーム一覧パネル

    [Header("ルーム管理用")]
    [SerializeField] Room originalRoomButton;                                             //ルームボタン格納
    [SerializeField] GameObject roomButtonContent;                                        //ルームボタンの親オブジェクト
    Dictionary<string, RoomInfo> roomsList = new Dictionary<string, RoomInfo>();          //ルームの情報を扱う辞書
    List<Room> allRoomButtons = new List<Room>();                                         //ルームボタンを扱うリスト

    [Header("Player管理用")]
    [SerializeField] Text playerNameText;                             //プレイヤーテキスト
    List<Text> allPlayerNames = new List<Text>();                     //プレイヤーの管理リスト
    [SerializeField] GameObject playerNameContent;                    //プレイヤーネームの親オブジェクト
    [SerializeField] GameObject nameInputPanel;                       //名前入力パネル
    [SerializeField] Text placeholderText;                            //表示テキスト、
    [SerializeField] InputField nameInput;                            //名前入力フォーム
    bool setName;                                                     //名前入力判定

    [Header("ゲーム開始用")]
    [SerializeField] GameObject startButton;  //ゲーム開始するためのボタン

    [Header("遷移先のシーン")]
    [SerializeField] string levelToPlay;      //遷移先のシーン名


    void Awake()
    {
        if(instance ==  null)
            instance = this;
        else 
            Destroy(this.gameObject);
    }


    void Start()
    {
        //メニューを全て閉じる
        CloseMenuUI();

        AAA();
    }

    void AAA()
    {
        //ロードパネルを表示してテキスト更新
        loadingPanel.SetActive(true);
        loadingText.text = "ネットワークに接続中...";

        //To DO : off-line環境へ戻るか選択肢を表示するよう変更する
        if (!IsInternetConnected())
        {
            loadingText.text = "ネットワークの接続に失敗したため実行できません。";
            Debug.LogError("Error:NotConnect Internet");
        }

        if (!PhotonNetwork.IsConnected)
        {
            //PhotonServerSettingsファイルの設定に従ってPhotonに接続
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// ネットワーク接続確認
    /// </summary>
    bool IsInternetConnected()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return false;
        else
            return true;
    }


    /// <summary>
    /// メニューを全て閉じる
    /// </summary>
    void CloseMenuUI()
    {
        //ロードパネル非表示
        loadingPanel.SetActive(false);

        //ボタン非表示
        buttons.SetActive(false);

        //ルーム作成パネル
        createRoomPanel.SetActive(false);

        //ルームパネル
        roomPanel.SetActive(false);

        //エラーパネル
        errorPanel.SetActive(false);

        //ルーム一覧パネル
        roomListPanel.SetActive(false);
        
        //名前入力パネル
        nameInputPanel.SetActive(false);
    }


    /// <summary>
    /// クライアントがMaster Serverに接続+準備が整ったとき
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //マスターサーバー上のデフォルトロビーに参加
        PhotonNetwork.JoinLobby();

        //テキスト更新
        loadingText.text = "ロビーへ参加...";

        //マスターと同じシーンに行くように設定
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    /// <summary>
    /// マスターサーバーのロビーに入るときに呼び出し
    /// </summary>
    public override void OnJoinedLobby()
    {
        //ロビーメニューを表示
        LobbyMenuDisplay();

        //辞書の初期化
        roomsList.Clear();

        //仮のユーザーネームを決定
        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

        //名前が入力されている場合はその名前を反映
        ConfirmationName();
    }


    /// <summary>
    /// ロビーメニュー表示
    /// </summary>
    public void LobbyMenuDisplay()
    {
        //メニュー閉じる
        CloseMenuUI();

        //ボタンらを表示
        buttons.SetActive(true);
    }


    /// <summary>
    /// ルーム作成画面表示
    /// </summary>
    public void OpenCreateRoomPanel()
    {
        //メニュー閉じる
        CloseMenuUI();
        createRoomPanel.SetActive(true);
    }


    /// <summary>
    /// （制作画面）ルーム作成
    /// </summary>
    public void CreateRoomButton()
    {
        //インプットフィールドのテキストに何か入力されてるか
        if (!string.IsNullOrEmpty(enterRoomName.text))
        {
            //ルームのオプションをインスタンス化して変数に代入
            RoomOptions options = new RoomOptions();

            //プレイヤーの最大参加人数の設定
            options.MaxPlayers = ROOM_MEMBER_MAX;

            //ルームを生成(ルーム名：部屋の設定)
            PhotonNetwork.CreateRoom(enterRoomName.text, options);

            //メニュー閉じる
            CloseMenuUI();

            //テキスト更新
            loadingText.text = "ルーム作成中...";

            //読み込みパネル表示
            loadingPanel.SetActive(true);
        }
    }


    /// <summary>
    /// ルーム参加時に実行される処理
    /// </summary>
    public override void OnJoinedRoom()
    {
        //メニュー閉じる
        CloseMenuUI();

        //ルームパネル表示
        roomPanel.SetActive(true);

        //現在いるルームを取得し、テキストにルーム名を反映
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        //ルームに参加しているプレイヤーを表示
        GetAllPlayer();

        //ルームマスターか判定する
        CheckRoomMaster();
    }


    /// <summary>
    /// 参加中のルームから退出
    /// </summary>
    public void LeavRoom()
    {
        //現在のルームから退出
        PhotonNetwork.LeaveRoom();

        //メニュー閉じる
        CloseMenuUI();

        //テキストを更新
        loadingText.text = "退出中・・・";

        //読み込み中の画面を表示
        loadingPanel.SetActive(true);
    }


    /// <summary>
    /// ルーム退出時実行
    /// </summary>
    public override void OnLeftRoom()
    {
        // 別の物を表示するなら必要かも？

        //ロビーメニューを表示
        //LobbyMenuDisplay();
    }


    /// <summary>
    /// サーバにルーム作成失敗時実行
    /// </summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //エラーテキストを編集
        errorText.text = "ルームの作成に失敗しました" + message;

        //メニュー閉じる
        CloseMenuUI();

        //エラー画面表示
        errorPanel.SetActive(true);
    }


    /// <summary>
    /// ルーム一覧画面を表示
    /// </summary>
    public void FindRoom()
    {
        //メニュー閉じる
        CloseMenuUI();

        //ルーム一覧画面を表示
        roomListPanel.SetActive(true);
    }


    /// <summary>
    /// Master Serverのロビー待機中のルームリスト更新
    /// </summary>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //ルームUIの初期化
        RoomUIinitialization();

        //ルーム情報を辞書に格納
        UpdateRoomList(roomList);
    }


    /// <summary>
    /// ルームの情報を辞書に
    /// </summary>
    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        //ルームの数分ループ
        for (int i = 0; i < roomList.Count; i++)
        {
            //ルーム情報を変数に格納
            RoomInfo info = roomList[i];

            //ロビーで使用され、リストされなくなった部屋をマーク（満室、閉鎖、または非表示）
            if (info.RemovedFromList)
            {
                //辞書から削除
                roomsList.Remove(info.Name);
            }
            else
            {
                //ルーム名をキーにして、辞書に追加
                roomsList[info.Name] = info;
            }
        }

        //辞書にあるすべてのルームを表示
        RoomListDisplay(roomsList);
    }


    /// <summary>
    /// ルーム表示
    /// </summary>
    void RoomListDisplay(Dictionary<string, RoomInfo> cachedRoomList)
    {
        //辞書のキー(値)で回す
        foreach (var roomInfo in cachedRoomList)
        {
            //ルームボタン作成
            Room newButton = Instantiate(originalRoomButton);

            //生成したボタンにルームの情報を設定
            newButton.RegisterRoomDetails(roomInfo.Value);

            //生成したボタンに親の設定
            newButton.transform.SetParent(roomButtonContent.transform);

            //リストに追加
            allRoomButtons.Add(newButton);
        }
    }


    /// <summary>
    /// ルームボタンUI初期化
    /// </summary>
    void RoomUIinitialization()
    {
        // ルームオブジェクトの数分ループ
        foreach (Room rm in allRoomButtons)
        {
            // ボタンオブジェクトを削除
            Destroy(rm.gameObject);
        }

        //リスト要素削除
        allRoomButtons.Clear();
    }


    /// <summary>
    /// 引数のルームに入る
    /// </summary>
    public void JoinRoom(RoomInfo roomInfo)
    {
        //引数のルームに参加
        PhotonNetwork.JoinRoom(roomInfo.Name);

        //メニュー閉じる
        CloseMenuUI();

        //テキストを編集
        loadingText.text = "ルーム参加中...";

        //パネルを表示する
        loadingPanel.SetActive(true);
    }


    /// <summary>
    /// ルームにいるプレイヤーを取得
    /// </summary>
    public void GetAllPlayer()
    {
        //初期化
        InitializePlayerList();

        //プレイヤー表示
        PlayerDisplay();
    }


    /// <summary>
    /// プレイヤー一覧初期化
    /// </summary>
    void InitializePlayerList()
    {
        //リストで管理している数分ループ
        foreach (var rm in allPlayerNames)
        {
            //text削除
            Destroy(rm.gameObject);
        }

        //リスト初期化
        allPlayerNames.Clear();
    }


    /// <summary>
    /// ルームにいるプレイヤーを表示
    /// </summary>
    void PlayerDisplay()
    {
        //ルームに接続しているプレイヤーの数分ループ
        foreach (var players in PhotonNetwork.PlayerList)
        {
            //テキストの生成
            PlayerTextGeneration(players);
        }
    }


    /// <summary>
    /// プレイヤーテキスト生成
    /// </summary>
    void PlayerTextGeneration(Player players)
    {
        //用意してあるテキストをベースにプレイヤーテキストを生成
        Text newPlayerText = Instantiate(playerNameText);

        //テキストに名前を反映
        newPlayerText.text = players.NickName;

        //親オブジェクトの設定
        newPlayerText.transform.SetParent(playerNameContent.transform);

        //リストに追加
        allPlayerNames.Add(newPlayerText);
    }


    /// <summary>
    /// 名前の判定
    /// </summary>
    void ConfirmationName()
    {
        //名前が入力されていないなら
        if (!setName)
        {
            //メニュー閉じる
            CloseMenuUI();

            //名前入力パネルを表示
            nameInputPanel.SetActive(true);

            //キーが保存されているか確認
            if (PlayerPrefs.HasKey("playerName"))
            {
                placeholderText.text = PlayerPrefs.GetString("playerName");

                //インプットフィールドに名前を表示
                nameInput.text = PlayerPrefs.GetString("playerName");
            }

        }
        else//既に入力されている場合は自動的に名前をセット
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }
    }


    /// <summary>
    /// 名前保存や入力判定切り替え
    /// </summary>
    public void SetName()
    {
        //入力されている場合
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            //ユーザー名に入力された名前を反映
            PhotonNetwork.NickName = nameInput.text;

            //名前を保存
            PlayerPrefs.SetString("playerName", nameInput.text);

            //ロビーに戻る
            LobbyMenuDisplay();

            //名前入力済み判定
            setName = true;
        }
    }


    /// <summary>
    /// プレイヤーがルーム参加時に呼ばれる処理
    /// </summary>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //テキストを生成
        PlayerTextGeneration(newPlayer);
    }


    /// <summary>
    /// プレイヤーがルーム退出時に呼ばれる処理
    /// </summary>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //ルーム内プレイヤーリスト更新
        GetAllPlayer();
    }


    /// <summary>
    /// 部屋のマスターか確認
    /// </summary>
    void CheckRoomMaster()
    {
        //部屋のマスターか確認
        if (PhotonNetwork.IsMasterClient)
        {
            //部屋のマスターならゲーム開始ボタンを表示
            startButton.gameObject.SetActive(true);
        }
        else
        {
            //部屋のマスターでないならゲーム開始ボタンを非表示
            startButton.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// MasterClient終了時のMasterClient切り替え
    /// </summary>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //部屋のマスターか確認
        if (PhotonNetwork.IsMasterClient)
        {
            //部屋のマスターならゲーム開始ボタンを表示
            startButton.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// 指定シーンに遷移させる
    /// </summary>
    public void PlayGame()
    {
        //引数のステージを読み込み
        PhotonNetwork.LoadLevel(levelToPlay);
    }


    /// <summary>
    ///  ゲーム終了関数
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}