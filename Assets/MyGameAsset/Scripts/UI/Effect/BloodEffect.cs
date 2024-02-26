using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: 作り直す
// Event登録型でOnダメージ時のHPCallback実装に処理を付ける感じで良さげ！

public class BloodEffect : MonoBehaviour
{
    [Tooltip("血の画像")]
    [SerializeField] Image bloodImage;

    /// <summary>
    /// 血の画像
    /// </summary>
    /// <param name="maxhp">最大HP量</param>
    /// <param name="currentHp">現在のHP量</param>
    public void BloodUpdate(int maxhp, int currentHp)
    {
        // HPの割合を計算
        float hpRatio = (float)currentHp / maxhp;

        // アルファ値を計算
        float alphaValue = 1.0f - hpRatio; // 1.0 - HP割合でアルファ値を設定

        // アルファ値を変更
        Color imageColor = bloodImage.color;
        imageColor.a = alphaValue;
        bloodImage.color = imageColor;
    }
}