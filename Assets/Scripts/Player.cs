using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool moveLock = true;

    private bool firstTimeInventoryOpen = true;
    
    //private Vector2 _input;
    private CharacterController _characterController;
    private float speed = 50f;

    public Inventory inventory;
    
    [SerializeField] private UI_Inventory uI_Inventory;

    [SerializeField] private Animator baseAnim;
    [SerializeField] private Animator hairAnim;
    [SerializeField] private Animator eyeAnim;
    [SerializeField] private Transform eyeTrans;
    [SerializeField] private Transform trans;
    private int[] spriteArray = new int[3];

    public enum flags { defaultFlag, //flag put on all dialogue
            testFlag1, 
            testFlag2,
            testFlag3,
            testFlag4,
            testItemFlag,
            monologueFlag1,
            monologueFlag2//demo test flags
    };
    [SerializeField] private List<ConvoMetadata> knownDialoguesSaveLocation;
    
    public HashSet<Flag> dialogueFlags = new HashSet<Flag>();

    private void Start()
    {
        baseAnim = GetComponent<Animator>();

        /*spriteArray[0] = PlayerPrefs.GetInt("bodyIndex");
        spriteArray[1] = PlayerPrefs.GetInt("hairIndex");
        spriteArray[2] = PlayerPrefs.GetInt("hairCIndex") + spriteArray[1] * 5;
        spriteArray[3] = PlayerPrefs.GetInt("eyeIndex");
        spriteArray[4] = PlayerPrefs.GetInt("eyeCIndex") + spriteArray[3] * 5;*/

        spriteArray[0] = PlayerPrefs.GetInt("bodyIndex");
        spriteArray[1] = PlayerPrefs.GetInt("hairCIndex") + PlayerPrefs.GetInt("hairIndex") * 5;
        spriteArray[2] = PlayerPrefs.GetInt("eyeCIndex") + PlayerPrefs.GetInt("eyeIndex") * 5;

        baseAnim.SetFloat("BaseIndex", (float) spriteArray[0]);
        hairAnim.SetFloat("HairIndex", (float)spriteArray[1]);
        eyeAnim.SetFloat("EyeIndex", (float)spriteArray[2]);
        updateX(1);
        updateY(-1);

        inventory = new Inventory();

        _characterController = GetComponent<CharacterController>();

        SaveData data = SaveSystem.LoadPlayer();

        for(int i=0; i < data.flags.Length; i++)
        {
            dialogueFlags.Add((Flag)data.flags[i]);
        }

        for(int i=0; i < data.inventoryItems.Length; i++)
        {
            InventoryItem inventoryItemData = new InventoryItem();
            inventoryItemData.itemType = (InventoryItem.ItemType) data.inventoryItems[i];
            inventory.AddInventoryItem(inventoryItemData);
        }

        knownDialoguesSaveLocation = data.learnedDialogues;
        knownDialoguesSaveLocation ??= new List<ConvoMetadata> { };
        DialogueInventory.LoadData(this, in knownDialoguesSaveLocation);
        transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        moveLock = true;
        Invoke("moveLockOff", 0.5f);

        //baseAnim.Play(-1,0);
        //eyeAnim.Play(-1, 0);
    }

    private void updateX(float num)
    {
        baseAnim.SetFloat("X", num);
        hairAnim.SetFloat("X", num);
        eyeAnim.SetFloat("X", num);
    }
    private void updateY(float num)
    {
        baseAnim.SetFloat("Y", num);
        hairAnim.SetFloat("Y", num);

        if (num > 0)
        {
            //eyeAnim.SetLayerWeight(0,0);
            eyeTrans.position = trans.position + new Vector3(0, 0, 0.02f);
        }
        if (num < 0)
        {
            //eyeAnim.SetLayerWeight(0, 1);
            eyeTrans.position = trans.position + new Vector3(0, 0, -0.02f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveLock)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _characterController.Move(move * Time.deltaTime * speed);

            if(move.x != 0)
            {
                updateX(move.x);
            }
            if (move.z != 0)
            {
                updateY(move.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && firstTimeInventoryOpen)
        {
            firstTimeInventoryOpen = false;
            uI_Inventory.SetInventory(inventory);

        } else if (Input.GetKeyDown(KeyCode.I))
        {
            uI_Inventory.RefreshInventoryItems();
        }
    }

    void moveLockOff()
    {
        moveLock = false;
    }

    //testing purposes
    public void printFlags()
    {
        Flag[] flagsEnum = new Flag[dialogueFlags.Count];
        dialogueFlags.CopyTo(flagsEnum);
        string output = "";
        for (int i = 0; i < flagsEnum.Length; i++)
        {
            output += flagsEnum[i];
            output += " ";
        }
    }

    public void AddToInventory(InventoryItem inventoryItem)
    {
        inventory.AddInventoryItem(inventoryItem);
    }

    public void SaveLearnedDialogues(in List<ConvoMetadata> inList)
    {
        knownDialoguesSaveLocation = inList;
    }
}
