using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SaveData
{
    public bool hasSave = false;
    
    public float[] playerPosition;
    public int[] flags;
    public int[] inventoryItems;
    public List<ConvoMetadata> learnedDialogues;
    
    public SaveData(Player player) 
    { 
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;

        int len = player.dialogueFlags.Count;
        Flag[] flagsEnum = new Flag[len];
        player.dialogueFlags.CopyTo(flagsEnum);
        flags = new int[len];
        for(int i=0; i < len; i++)
        {
            flags[i] = (int)flagsEnum[i];
        }

        int lenInventory = player.inventory.GetItemList().Count;
        InventoryItem[] flagsInventoryEnum = player.inventory.GetItemList().ToArray();
        inventoryItems = new int[lenInventory];


        for(int i=0; i < lenInventory; i++)
        {
            inventoryItems[i] = (int) flagsInventoryEnum[i].itemType;
        }

    }

    public SaveData()
    {
        playerPosition = new float[3];
        playerPosition[0] = 0;
        playerPosition[1] = 21; //19 for human
        playerPosition[2] = 0;
        flags = new int[1];
        flags[0] = 0;
        inventoryItems = new int[0];
    }
}
