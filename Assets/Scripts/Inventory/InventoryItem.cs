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

    public ItemType itemType;

    public string itemDescription;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.None:             
                return null;
            default:
                return ItemAssets.Instance.Sprites[itemType];
        }
    }

    public string GetItemDescription()
    {
        return itemDescription;
    }


}
