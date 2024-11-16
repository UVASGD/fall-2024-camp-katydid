using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        SpritesDict = new Dictionary<InventoryItem.ItemType, Sprite>();
        foreach (var pair  in sprites)
        {
            SpritesDict.Add(pair.type, pair.sprite);
        }
        
    }

    [Serializable]
    public struct ItemTypeSprite
    {
        public InventoryItem.ItemType type;
        public Sprite sprite;

    }

    public List<ItemTypeSprite> sprites;
    
    public Dictionary<InventoryItem.ItemType, Sprite> SpritesDict;
    
}
