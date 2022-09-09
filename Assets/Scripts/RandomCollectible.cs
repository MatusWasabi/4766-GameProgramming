using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectible : MonoBehaviour
{
    public CollectibleColor color;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int randomNumber;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        randomNumber = Random.Range(0, sprites.Length);

        switch (randomNumber)
        {
            case 0:
                color = CollectibleColor.Red;
                break;
            case 1:
                color = CollectibleColor.Green;
                break;
            case 2:
                color = CollectibleColor.Blue;
                break;
        }

        spriteRenderer.sprite = sprites[randomNumber];
        
        /*randomNumber = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[randomNumber];
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
