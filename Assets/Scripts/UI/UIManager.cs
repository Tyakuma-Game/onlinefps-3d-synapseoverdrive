using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI管理クラス
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("参照")]

    [Tooltip("弾薬の所持数テキスト")]
    [SerializeField] Text bulletsIHaveText;

    [Tooltip("弾薬の装填数と最大装填可能数")]
    [SerializeField] Text bulletText;

    [Tooltip("HPスライダー格納")]
    [SerializeField] Slider hpSlider;

    [Tooltip("HPのテキスト格納")]
    [SerializeField] Text hpText;

    [Tooltip("死亡パネル")]
    [SerializeField] GameObject deathPanel;

    [Tooltip("デステキスト")]
    [SerializeField] Text deathText;

    [Tooltip("スコアボードUI")]
    [SerializeField] GameObject scoreboard;

    [Tooltip("プレイヤー情報セットスクリプト")]
    [SerializeField] PlayerInformation info;

    [Tooltip("終了パネル")]
    [SerializeField] GameObject endPanel;

    [Tooltip("タイトル画面に戻るか選択する画面")]
    [SerializeField]
    GameObject returnToTitlePanel;


    /// <summary>
    /// 装備中の銃の弾数反映
    /// </summary>
    /// <param name="ammoClip">マガジン内弾薬数</param>
    /// <param name="ammoClip">マガジン内弾薬数</param>
    /// <param name="ammunition">所持弾薬数</param>
    public void SettingBulletsText(int ammoClipMax,int ammoClip, int ammunition)
    {
        bulletsIHaveText.text = "×"+ammunition.ToString();
        bulletText.text = ammoClip + "/" + ammoClipMax;
    }


    /// <summary>
    /// HPの更新
    /// </summary>
    /// <param name="maxhp">最大HP</param>
    /// <param name="currentHp">現在のHP</param>
    public void UpdateHP(int maxhp, int currentHp)
    {
        hpText.text = $"{currentHp.ToString()} / {maxhp.ToString()}";
        hpSlider.maxValue = maxhp;
        hpSlider.value = currentHp;
    }


    /// <summary>
    /// デスUI更新
    /// </summary>
    /// <param name="name">倒したプレイヤーの名前</param>
    public void UpdateDeathUI(string name)
    {
        //表示
        deathPanel.SetActive(true);

        //テキスト更新
        deathText.text = name + "にやられた";

        //一定時間後に閉じる
        Invoke("CloseDeathUI", 5f);
    }


    /// <summary>
    /// 落下してしまった際のデスUI表示
    /// </summary>
    public void UpdateDeathUI()
    {
        //表示
        deathPanel.SetActive(true);

        //テキスト更新
        deathText.text = "世界の狭間に飲み込まれてしまった...";

        //一定時間後に閉じる
        Invoke("CloseDeathUI", 5f);
    }


    /// <summary>
    /// デスUI非表示
    /// </summary>
    private void CloseDeathUI()
    {
        deathPanel.SetActive(false);
    }


    /// <summary>
    /// キルデス表の表示切替
    /// </summary>
    public void ChangeScoreUI()
    {
        //表示していたら非表示に、非表示なら表示
        scoreboard.SetActive(!scoreboard.activeInHierarchy);
    }


    /// <summary>
    /// タイトルに戻るか選択画面の表示切替
    /// </summary>
    public void ChangeReturnToTitlePanel()
    {
        //表示していたら非表示に、非表示なら表示
        returnToTitlePanel.SetActive(!returnToTitlePanel.activeInHierarchy);
    }


    /// <summary>
    /// ゲーム終了パネル表示
    /// </summary>
    public void OpenEndPanel()
    {
        endPanel.SetActive(true);
    }


    /// <summary>
    /// PlayerInformationの情報を取得
    /// </summary>
    public PlayerInformation GetPlayerInformation()
    {
        return this.info;
    }
}