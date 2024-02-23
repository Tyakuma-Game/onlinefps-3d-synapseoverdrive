using UnityEngine;

/// <summary>
/// プレイヤーの移動状態
/// </summary>
public enum PlayerMoveState
{
    Idol,
    Walk,
    Run
}


/// <summary>
/// プレイヤーの状態一覧
/// </summary>
public enum PlayerAnimationState
{
    Idol,   // 待機状態
    Walk,   // 歩き状態
    Run,    // 走り状態
    Jump    // ジャンプ状態
}


public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource walkSound;
    [SerializeField] AudioSource runSound;
    [SerializeField] AudioSource damegeSound;

    public void DamageSound()
    {
        damegeSound.Stop();
        damegeSound.Play();
    }

    public void SoundPlays(PlayerAnimationState playerAnimationState)
    {
        // TO DO : 仮のSound管理（後で修正する）
        if (playerAnimationState == PlayerAnimationState.Run)
        {
            if (!runSound.isPlaying)
            {
                runSound.loop = true;
                runSound.Play();
            }
        }
        else
        {
            if (runSound.isPlaying)
            {
                runSound.loop = false;
                runSound.Stop();
            }
        }

        if (playerAnimationState == PlayerAnimationState.Walk)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.loop = true;
                walkSound.Play();
            }
        }
        else
        {
            if (walkSound.isPlaying)
            {
                walkSound.loop = false;
                walkSound.Stop();
            }
        }
    }
}