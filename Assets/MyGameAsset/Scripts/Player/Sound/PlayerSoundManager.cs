using UnityEngine;

// TODO:
// 一旦後回しで演出を先に入れる
// 別のクラスに以降する？

/// <summary>
/// プレイヤーの音に関する処理を管理するクラス
/// </summary>
public class PlayerSoundManager : MonoBehaviour
{
    // 追加音
    // ジャンプ
    // 着地

    [SerializeField] AudioSource walkSound;
    [SerializeField] AudioSource sprintSound;
    [SerializeField] AudioSource damageSound;


    void Start()
    {
        // 処理登録
        PlayerEvent.OnDamage += OnDamage;
    }

    void OnDestroy()
    {
        // 処理解除
        PlayerEvent.OnDamage -= OnDamage;
    }

    /// <summary>
    /// ダメージを受けた際の音
    /// </summary>
    void OnDamage()
    {
        damageSound.Stop();
        damageSound.Play();
    }

    /// <summary>
    /// 歩き状態の音
    /// </summary>
    void OnWalk()
    {
        // 停止
        sprintSound.loop = false;
        sprintSound.Stop();

        // 再生
        walkSound.loop = true;
        walkSound.Play();
    }

    /// <summary>
    /// 疾走状態の音
    /// </summary>
    void OnSprint()
    {
        // 停止
        walkSound.loop = false;
        walkSound.Stop();

        // 自分の再生
        sprintSound.loop = true;
        sprintSound.Play();
    }
}