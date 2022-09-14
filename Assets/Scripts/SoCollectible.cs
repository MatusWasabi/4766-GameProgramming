using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptables Objects/Collectible")]
public class SoCollectible : ScriptableObject
{
    [SerializeField] private string collectibleName;
    [SerializeField] private CollectibleColor collectibleColor;

    public string GetName()
    {
        return collectibleName;
    }
}
