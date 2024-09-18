using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellParticleSound : MonoBehaviour
{
    public AudioClip shellSound;
    [Range(0, 1)]
    public float shellSoundVolume = 0.5f;
    bool allowPlaySound = true;
    private void OnEnable()
    {
        allowPlaySound = true;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (!allowPlaySound)
            return;

        SoundManager.PlaySfx(shellSound, shellSoundVolume);

        allowPlaySound = false;
    }
}
