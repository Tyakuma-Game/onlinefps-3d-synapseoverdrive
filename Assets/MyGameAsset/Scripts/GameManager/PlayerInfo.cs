using System;

/// <summary>
/// Player情報管理クラス
/// </summary>
[Serializable]
public class PlayerInfo
{
    public string name;             //名前
    public int actor, kills, deaths;//番号、キル、デス

    //情報格納
    public PlayerInfo(string _name, int _actor, int _kills, int _death)
    {
        name = _name;
        actor = _actor;
        kills = _kills;
        deaths = _death;
    }
}