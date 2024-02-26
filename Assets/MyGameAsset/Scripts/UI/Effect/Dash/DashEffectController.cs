using UnityEngine;

/// <summary>
/// ダッシュエフェクトの管理クラス
/// </summary>
public class DashEffectController : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] GameObject dashEffect;

    void Start()
    {
        PlayerMove.OnStateChanged += SetDashEffectActive;
    }

    void OnDestroy()
    {
        PlayerMove.OnStateChanged -= SetDashEffectActive;
    }

    /// <summary>
    /// ダッシュエフェクト表示切替
    /// </summary>
    /// <param name="isActive">表示状態</param>
    void SetDashEffectActive(bool isActive) =>
        dashEffect.SetActive(isActive);
}