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
    [SerializeField] private AudioClip collectibleSound;
    [SerializeField] private ParticleSystem collectedParticle;
    [SerializeField] private ParticleSystem respawnParticle;

    public AudioClip collectedSound
    {
        get => collectibleSound;
        private set { collectibleSound = value; }
    }

    private void Start()
    {
        powerUp = soCollectible.GetPowerUp();

        collectibleSprite = GetComponent<SpriteRenderer>();
        collectibleSprite.sprite = soCollectible.GetSprite();

        if (TryGetComponent(out RandomCollectible randomCollectible))
        {
            color = randomCollectible.color;
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
            gameObject.SetActive(false);
            respawning.RespawnItem();
        }
    }

    private void OnDisable()
    {
        collectedParticle.gameObject.transform.position = gameObject.transform.position;
        collectedParticle.Play();
    }

    private void OnEnable()
    {
        respawnParticle.gameObject.transform.position = gameObject.transform.position;
        respawnParticle.Play();
    }






}
