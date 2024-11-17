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
        {ItemType.batteries, "AA Batteries: For Drew's Walkman"},
        {ItemType.medKey, "Med Cabinet Key: investigate the med cabinet"},
        {ItemType.sleepPill, "Sleeping pill bottle: it's empty"},
        {ItemType.blankBit, "Ripped Blanket Piece: Must be from the Rose Cabin."},
        {ItemType.foolCard, "The Fool Tarot Card: Is Cletus involved?"},
        {ItemType.jellyBeans, "Jelly Beans: Earl told me to bring these to the lake. Why would a kraken like jelly beans?"},
        {ItemType.receiptOne, "Jelly Beans Receipt, Dylan: Talk to Dylan"},
        {ItemType.receiptTwo, "Jelly Beans Receipt, Dennis: Talk to Dennis."},
         {ItemType.candy, "Caramel Candy: Earl's favorite candy"},
        {ItemType.tNote, "�It�s done� Note: To Vanessa, from Tony."},
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
