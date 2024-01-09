using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    [Header("定数")]
    [Tooltip("スポーンするまでの猶予時間")]
    [SerializeField] float RESPAWN_INTERVAL = 5f;
    

    [Header("参照")]
    [Tooltip("スポーンポイントのオブジェクト格納用配列")]
    [SerializeField] Transform[] spawnPositons;

    [Tooltip("生成するオブジェクト")]
    [SerializeField] GameObject playerPrefab;

    [Tooltip("プレイヤーの階層管理用")]
    [SerializeField] GameObject Players;

    [Tooltip("生成したプレイヤーを格納")]
    GameObject player;

    GameObject aaa;


    void Start()
    {
        //スポーンポイントオブジェクトをすべて非表示に
        foreach (var pos in spawnPositons)
        {
            pos.gameObject.SetActive(false);
        }

        //ネットワークに接続されているなら
        if (PhotonNetwork.IsConnected)
        {
            //プレイヤー生成
            SpawnPlayer();
        }
    }



    /// <summary>
    /// リスポーン地点をランダム取得
    /// </summary>
    public Transform GetSpawnPoint()
    {
        //ランダムでスポーンポイントを１つ選んで位置情報を返す
        return spawnPositons[Random.Range(0, spawnPositons.Length)];
    }


    /// <summary>
    /// Playerを生成
    /// </summary>
    public void SpawnPlayer()
    {
        //スポーン座標をリストの中からランダムに取得
        Transform spawnPoint = GetSpawnPoint();

        //ネットワーク上にプレイヤーを生成
        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);//削除用に保存
    }

    void DestryNetWarkPlayer()
    {
        //playerをネットワーク上から削除
        PhotonNetwork.Destroy(player);
    }

    /// <summary>
    /// Playerのリスポーン処理
    /// </summary>
    public void Die()
    {
        if (player != null)
        {
            //一定時間後プレイヤーをスポーン
            Invoke("SpawnPlayer", RESPAWN_INTERVAL);
        }

        //一定時間後プレイヤーを破壊
        Invoke("DestryNetWarkPlayer", RESPAWN_INTERVAL-0.5f);
    }
}