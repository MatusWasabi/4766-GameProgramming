using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    [SerializeField] private float waitingSeconds;
    [SerializeField] private GameObject respawningItem;


    public void RespawnItem ()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
            yield return new WaitForSeconds(waitingSeconds);
            respawningItem.SetActive(true);
    }
}
