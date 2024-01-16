using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIイメージの点滅処理クラス
/// </summary>
public class UILogoImageFlicker : MonoBehaviour
{
    [Tooltip("対象のイメージ")]
    [SerializeField] Image image;

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
        currentAlpha = image.color.a;
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


    public void SetMacAlpha(float tmpAlpha)
    {
        maxAlpha = tmpAlpha;
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
    /// 透明度（α）値更新
    /// </summary>
    void UpdateAlphaValue()
    {
        float deltaAlpha = flickerSpeed * Time.deltaTime * (increasing ? 1 : -1);
        currentAlpha = Mathf.Clamp(currentAlpha + deltaAlpha, minAlpha, maxAlpha);
    }

    /// <summary>
    /// 新規透明率適用
    /// </summary>
    void ApplyNewAlpha()
    {
        Color newColor = image.color;
        newColor.a = currentAlpha;
        image.color = newColor;
    }
}