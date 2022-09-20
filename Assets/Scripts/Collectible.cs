using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleColor color;
    [SerializeField] private SoCollectible soCollectible;
    [SerializeField] private Respawning respawning;
    public PowerUp powerUp;




    private void Start()
    {
        powerUp = soCollectible.GetPowerUp();

        if (TryGetComponent(out RandomCollectible randomCollectible))
        {
            color = randomCollectible.color;
        }

        if (soCollectible != null) 
        {
            Debug.Log($"SoCollectible name is {soCollectible.GetName()}"); 

        }
        

    }


    private void OnDisable()
    {
        respawning.RespawnItem();
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
