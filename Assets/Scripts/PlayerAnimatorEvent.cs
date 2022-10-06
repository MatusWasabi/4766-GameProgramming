using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvent : MonoBehaviour
{
    [SerializeField] private PlayerAudioController playerAudioController;

    public void WalkSound()
    {
        playerAudioController.PlayWalkSound();
    }
}
