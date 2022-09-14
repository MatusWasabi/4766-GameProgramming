using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleColor color;
    [SerializeField] private SoCollectible soCollectible;


    private void Start()
    {
        if (TryGetComponent(out RandomCollectible randomCollectible))
        {
            color = randomCollectible.color;
        }

        if (soCollectible != null) {Debug.Log($"SoCollectible name is {soCollectible.GetName()}"); }
        
    }




    /*
    CollectibleColor getColor()
    {
        return this.color;
    }

    void setColor(CollectibleColor color) 
    {
        this.color = color;
    }
    */
   

}
