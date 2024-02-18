using UnityEngine;
using Photon.Pun;
using System.Collections;

/// <summary>
/// プレイヤーのスポーンに関する処理を管理するクラス
/// </summary>
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }

    [Header(" Settings ")]
    [SerializeField] float respawnInterval = 5f;

    [Header(" Elements ")]
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject playerPrefab;
    GameObject playerInstance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // スポーンポイントを初期化して非表示に
        InitializeSpawnPoints();
    }

    void Start()
    {
        // ネットワーク接続がある場合のみプレイヤーをスポーン
        if (PhotonNetwork.IsConnected)
            SpawnPlayer();
    }

    /// <summary>
    /// スポーンポイントの初期化・非表示に設定
    /// </summary>
    void InitializeSpawnPoints()
    {
        foreach (var pos in spawnPositions)
            pos.gameObject.SetActive(false);
    }

    /// <summary>
    /// プレイヤーをネットワーク上に生成し、位置を設定
    /// </summary>
    public void SpawnPlayer()
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }

    /// <summary>
    /// プレイヤーが死亡した際の処理　リスポーンまでの一連の流れ開始
    /// </summary>
    public void StartRespawnProcess()
    {
        StartCoroutine(RespawnPlayer());
    }

    /// <summary>
    /// プレイヤーのリスポーンを管理するコルーチン
    /// </summary>
    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnInterval);   // 死亡演出の為の待ち時間
        PhotonNetwork.Destroy(playerInstance);
        SpawnPlayer();
    }

    /// <summary>
    /// 使用可能なスポーンポイントからランダムに一つ選択
    /// </summary>
    Transform GetRandomSpawnPoint()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Length)];
    }
}