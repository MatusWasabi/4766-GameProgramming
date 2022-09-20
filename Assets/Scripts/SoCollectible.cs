using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptables Objects/Collectible")]
public class SoCollectible : ScriptableObject
{
    [SerializeField] public string collectibleName { get; private set; }
    [SerializeField] private PowerUp powerUp;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite outlineSprite;
    [SerializeField] private bool isRespawnable;


    public string GetName()
    {
        return collectibleName;
    }

    public PowerUp GetPowerUp()
    {
        return powerUp;
    }
}
