using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    // ReSharper disable InconsistentNaming
    public enum ItemType
    {
        None = 0,
        batteries,
        medKey, // in game
        sleepPill, // in game
        foolCard,
        blankBit, // in game
        jellyBeans,
        receiptOne,
        receiptTwo,
        candy,
        tNote,
    }

    
    public InventoryItem()
    {
        this.itemType = ItemType.None;
    }
    
    public InventoryItem(ItemType itemType)
    {
        this.itemType = itemType;
    }

    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.None:             
                return null;
            default:
                if (ItemAssets.Instance.SpritesDict.ContainsKey(itemType))
                {
                    return ItemAssets.Instance.SpritesDict[itemType];
                }

                return null;
        }
    }

    public static Dictionary<ItemType, String> ItemDescriptions = new()
    {
        {ItemType.foolCard, "The Fool Tarot Card: Is Cletus involved?"}
    };

    public string GetItemDescription()
    {
        string ret;
        if (!ItemDescriptions.TryGetValue(itemType, out ret))
        {
            return "";
        }

        if (ret.Equals(""))
        {
            return "";
        }
        return ItemDescriptions[itemType];
    }


}
