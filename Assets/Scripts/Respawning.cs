using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    [SerializeField] private float waitingSeconds;
    [SerializeField] private GameObject respawningItem;
    [SerializeField] private AudioClip respawnSound;
    [SerializeField] private AudioSource audioSource;

    public void RespawnItem ()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(waitingSeconds);
        audioSource = FindObjectOfType<AudioSource>();
        respawningItem.SetActive(true);
        audioSource.PlayOneShot(respawnSound);
    }
}
