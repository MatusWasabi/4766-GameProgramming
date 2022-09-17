using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptables Objects/Collectible")]
public class SoCollectible : ScriptableObject
{
    [SerializeField] private string collectibleName;
    [SerializeField] private PowerUp powerUp;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite outlineSprite;
    [SerializeField] private bool isRespawnable;


    public string GetName()
    {
        return collectibleName;
    }
}
