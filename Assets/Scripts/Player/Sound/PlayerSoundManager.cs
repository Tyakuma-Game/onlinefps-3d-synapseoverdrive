using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField]AudioSource walkSound;
    [SerializeField] AudioSource runSound;

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