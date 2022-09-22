using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptables Objects/Collectible")]
public class SoCollectible : ScriptableObject
{
    [SerializeField] public string collectibleName { get; private set; }
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite outlineSprite;
    [SerializeField] private bool isRespawnable;
    [SerializeField] private PowerUp powerUp;

    public string GetName()
    {
        return collectibleName;
    }

    public PowerUp GetPowerUp()
    {
        return powerUp;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public Sprite GetOutlineSprite()
    {
        return outlineSprite;
    }
}
