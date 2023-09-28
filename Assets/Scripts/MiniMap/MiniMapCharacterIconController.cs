using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミニマップ用のアイコン管理クラス
/// </summary>
public class MiniMapCharacterIconController : MonoBehaviour
{
    [Tooltip("ミニマップアイコンの座標")]
    Transform iconTransform;

    [Tooltip("Y座標の定数")]
    [SerializeField] float yPositionConstant = 0.0f;


    void Awake()
    {
        // 座標取得
        iconTransform = this.transform;
    }


    /// <summary>
    /// ミニマップアイコンの更新処理
    /// </summary>
    public void MiniMapIconUpdate(Vector3 _playerPos)
    {
        // ミニマップ上での位置を計算
        Vector3 miniMapPos = new Vector3(_playerPos.x, yPositionConstant, _playerPos.z);

        // ミニマップアイコンの位置を更新
        iconTransform.position = miniMapPos;
    }
}