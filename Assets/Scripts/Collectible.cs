using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleColor color;


    private void Start()
    {
        if (TryGetComponent(out RandomCollectible randomCollectible))
        {
            color = randomCollectible.color;
        }
    }
    CollectibleColor getColor()
    {
        return this.color;
    }

    void setColor(CollectibleColor color) 
    {
        this.color = color;
    }

   

}
