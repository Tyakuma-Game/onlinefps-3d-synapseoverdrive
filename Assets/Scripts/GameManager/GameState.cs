using System;

/// <summary>
/// ゲームの状態
/// </summary>
[Serializable]
public enum GameState
{
    Playing,    //ゲームプレイ中
    Ending      //ゲーム終了
}