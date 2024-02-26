using System.Collections;
using UnityEngine;

/// <summary>
/// 銃のEffect管理用クラス
/// アニメーション側から呼び出しを行う
/// </summary>
public class GunEffects : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField]float lightDuration = 0.1f;
    [SerializeField]float particlesDuration = 1.0f;

    [Header(" Elements ")]
    [SerializeField] Light shootingLight;
    [SerializeField] ParticleSystem shootingParticles;

    /// <summary>
    /// 発射用ライトをONにし、指定時間後にOFFにする
    /// </summary>
    public void TurnOnShootingLight()
    {
        StartCoroutine(TurnOffLightAfterDelay(lightDuration));
        shootingLight.enabled = true;
    }

    /// <summary>
    /// 指定時間後に発射用ライトをOFFにするコルーチン
    /// </summary>
    IEnumerator TurnOffLightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shootingLight.enabled = false;
    }

    /// <summary>
    /// パーティクルを再生し、指定時間後に停止する
    /// </summary>
    public void PlayShootingParticles()
    {
        StartCoroutine(StopParticlesAfterDelay(particlesDuration));
        shootingParticles.Simulate(0.2f); // 0.2秒間再生を飛ばして開始する
        shootingParticles.Play();
    }

    /// <summary>
    /// 指定時間後にパーティクルを停止するコルーチン
    /// </summary>
    IEnumerator StopParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shootingParticles.Stop();
    }
}