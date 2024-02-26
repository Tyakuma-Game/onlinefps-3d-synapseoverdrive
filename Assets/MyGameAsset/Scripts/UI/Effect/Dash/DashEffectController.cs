using UnityEngine;

public class DashEffectController : MonoBehaviour
{
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
    /// ダッシュエフェクトの表示切替
    /// </summary>
    /// <param name="isActive">表示状態</param>
    void SetDashEffectActive(bool isActive) =>
        dashEffect.SetActive(isActive);
}