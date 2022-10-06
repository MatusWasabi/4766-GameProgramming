using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkAudioClips;
    

    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(walkAudioClips);
    }
    /*
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpAudioClips);
    }

    public void PlayFallSound()
    {
        audioSource.PlayOneShot(fallAudioClips);
    }


    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winAudioClips);
    }

    public void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadAudioClips);
    }
    */

    public void PlaySound(AudioClip soundPlayed)
    {
        audioSource.PlayOneShot(soundPlayed);
    }
}
