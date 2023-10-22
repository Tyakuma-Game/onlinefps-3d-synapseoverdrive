using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミニマップ処理の管理クラス
/// </summary>
public class MiniMapManager : MonoBehaviour
{
    [Header("参照")]
    [Tooltip("ミニマップ用のアイコン管理クラス")]
    [SerializeField] MiniMapCharacterIconController miniMapCharacterIconController;

    [Tooltip("ミニマップ用のカメラ管理クラス")]
    [SerializeField] MiniMapCameraController miniMapCameraController;

    [Tooltip("基準とするゲームオブジェクト")]
    [SerializeField] GameObject player;


    void Start()
    {
        // タグからPlayerを検索して保持
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        // キャラクターアイコンの座標更新
        miniMapCharacterIconController.MiniMapIconUpdate(player.transform.position);

        // ミニマップカメラの座標更新
        miniMapCameraController.MiniMapCameraUpdate(player.transform.position);
    }
}