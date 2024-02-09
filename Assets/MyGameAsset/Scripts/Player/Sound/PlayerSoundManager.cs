using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField]AudioSource walkSound;
    [SerializeField] AudioSource runSound;
    [SerializeField] AudioSource damegeSound;

    public void DamageSound()
    {
        damegeSound.Stop();
        damegeSound.Play();
    }

    public void SoundPlays(PlayerAnimationState playerAnimationState)
    {
        // TO DO : âºÇÃSoundä«óùÅiå„Ç≈èCê≥Ç∑ÇÈÅj
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