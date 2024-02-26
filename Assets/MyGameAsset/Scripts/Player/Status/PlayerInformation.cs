using UnityEngine;
using UnityEngine.UI;

// TODO: 作り直す

/// <summary>
/// Playerの詳細情報に関するクラス
/// </summary>
public class PlayerInformation : MonoBehaviour
{
    [Header("参照")]

    [Tooltip("プレイヤーの名前テキスト")]
    [SerializeField] Text playerNameText;

    [Tooltip("討伐数テキスト")]
    [SerializeField] Text kilesText;

    [Tooltip("死亡数テキスト")]
    [SerializeField] Text deathText;


    /// <summary>
    /// プレイヤーの詳細情報を格納する
    /// </summary>
    /// <param name="name">名前</param>
    /// <param name="kill">討伐数</param>
    /// <param name="death">死亡数</param>
    public void SetPlayerDetailes(string name, int kill, int death)
    {
        //各種データを格納
        playerNameText.text = name;
        kilesText.text = kill.ToString();
        deathText.text = death.ToString();
    }
}