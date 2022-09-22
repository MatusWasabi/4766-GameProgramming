using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleColor color;
    [SerializeField] private SoCollectible soCollectible;
    [SerializeField] private Respawning respawning;
    public PowerUp powerUp;
    private SpriteRenderer collectibleSprite;




    private void Start()
    {
        powerUp = soCollectible.GetPowerUp();

        collectibleSprite = GetComponent<SpriteRenderer>();
        collectibleSprite.sprite = soCollectible.GetSprite();

        if (TryGetComponent(out RandomCollectible randomCollectible))
        {
            color = randomCollectible.color;
        }

        if (soCollectible != null)
        {
            Debug.Log($"SoCollectible name is {soCollectible.GetName()}");

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"This Collectible is {soCollectible}");
            gameObject.SetActive(false);
            respawning.RespawnItem();
        }
    }




}
