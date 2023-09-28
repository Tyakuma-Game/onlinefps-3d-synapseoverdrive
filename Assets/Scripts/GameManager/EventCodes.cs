using System;

/// <summary>
/// 自作イベント：byteは扱うデータ(0 ～ 255)
/// </summary>
[Serializable]
public enum EventCodes : byte
{
    NewPlayer,      //新しいプレイヤー情報をマスターに送信
    ListPlayers,    //全員にプレイヤー情報を共有
    UpdateStat,     //キルデス数の更新
}