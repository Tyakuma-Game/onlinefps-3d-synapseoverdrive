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
    [SerializeField] Transform parentObject;
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

        // スポーンポイントを初期化
        InitializeSpawnPoints();
    }

    void Start()
    {
        // ネットワーク接続がある場合のみプレイヤーをスポーン
        if (PhotonNetwork.IsConnected)
            SpawnPlayer();
    }

    /// <summary>
    /// スポーンポイントの初期化
    /// </summary>
    void InitializeSpawnPoints()
    {
        foreach (var pos in spawnPositions)
            pos.gameObject.SetActive(false);
    }

    /// <summary>
    /// プレイヤーをネットワーク上に生成し、位置と親を設定
    /// </summary>
    public void SpawnPlayer()
    {
        // プレイヤー生成
        Transform spawnPoint = GetRandomSpawnPoint();
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        
        // 親オブジェクト設定
        if (parentObject != null)
            playerInstance.transform.SetParent(parentObject, false);
    }

    /// <summary>
    /// プレイヤーが死亡した際リスポーンまでの一連の処理
    /// </summary>
    public void StartRespawnProcess() =>
        StartCoroutine(RespawnPlayer());

    /// <summary>
    /// プレイヤーのリスポーンを管理するコルーチン
    /// </summary>
    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnInterval);   // 死亡演出用の待ち時間
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