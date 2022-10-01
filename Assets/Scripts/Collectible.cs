using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    public CollectibleColor color;
    [SerializeField] private SoCollectible soCollectible;
    [SerializeField] private Respawning respawning;
    public PowerUp powerUp;
    private SpriteRenderer collectibleSprite;
    [SerializeField] private GameObject endPoint;



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

        if (endPoint != null)
        {
            transform.DOMove(endPoint.transform.position, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
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

    private void FixedUpdate()
    {
        
        //(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo);
    }




}
