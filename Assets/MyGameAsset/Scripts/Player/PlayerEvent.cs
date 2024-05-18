using System;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// プレイヤーに関連するイベントを管理するクラス
/// Photonネットワークを使用してプレイヤーが生成された際に特定のイベントをトリガーする
/// </summary>
public class PlayerEvent : MonoBehaviour, IPunInstantiateMagicCallback
{
    /// <summary>
    /// ローカルプレイヤーがインスタンス化されたときに発生するイベント
    /// </summary>
    public static event Action OnPlayerInstantiated;

    /// <summary>
    /// プレイヤーがダメージを受けた時に発生するイベント
    /// </summary>
    public static Action OnDamage;

    /// <summary>
    /// プレイヤーがゲームにスポーン（生成）した時に発生するイベント
    /// </summary>
    public static Action onSpawn;

    /// <summary>
    /// プレイヤーがゲームから消滅（削除）した時に発生するイベント
    /// </summary>
    public static Action onDisappear;

    /// <summary>
    /// ネットワーク上でオブジェクトが生成された際にPhotonから自動的に呼び出されるメソッド
    /// ローカルプレイヤーの生成時に関連イベントを発火させる
    /// </summary>
    /// <param name="info">オブジェクト情報</param>
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal)
            OnPlayerInstantiated?.Invoke();
    }
}