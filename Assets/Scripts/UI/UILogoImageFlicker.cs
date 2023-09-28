using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIイメージの点滅処理クラス
/// </summary>
public class UILogoImageFlicker : MonoBehaviour
{
    [Tooltip("ロゴのイメージ")]
    [SerializeField] Image titleLogoImage;

    [Tooltip("アルファ値の最小値")]
    [SerializeField] float minAlpha = 0.2f;

    [Tooltip("アルファ値の最大値")]
    [SerializeField] float maxAlpha = 1.0f;

    [Tooltip("点滅速度")]
    [SerializeField] float flickerSpeed = 1.0f;

    [Tooltip("現在の透明度")]
    float currentAlpha;

    [Tooltip("透明度が増加中かどうかのフラグ")]
    bool increasing = true;

    void Start()
    {
        // 初期設定
        InitializeAlpha();
    }

    void Update()
    {
        // 透明度の増減状態更新
        UpdateFlickerState();

        // 透明度の増減状態更新
        UpdateAlphaValue();

        // 新規透明率適用
        ApplyNewAlpha();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void InitializeAlpha()
    {
        currentAlpha = titleLogoImage.color.a;
    }

    /// <summary>
    /// 透明度の増減状態更新
    /// </summary>
    void UpdateFlickerState()
    {
        if (currentAlpha >= maxAlpha)
        {
            increasing = false;
        }
        else if (currentAlpha <= minAlpha)
        {
            increasing = true;
        }
    }

    /// <summary>
    /// 透明度（α）値の更新
    /// </summary>
    private void UpdateAlphaValue()
    {
        float deltaAlpha = flickerSpeed * Time.deltaTime * (increasing ? 1 : -1);
        currentAlpha = Mathf.Clamp(currentAlpha + deltaAlpha, minAlpha, maxAlpha);
    }

    /// <summary>
    /// 新規透明率適用
    /// </summary>
    void ApplyNewAlpha()
    {
        Color newColor = titleLogoImage.color;
        newColor.a = currentAlpha;
        titleLogoImage.color = newColor;
    }
}