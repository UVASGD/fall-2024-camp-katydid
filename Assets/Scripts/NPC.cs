using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    
    
    private GameObject _player;
    private Player _playerScript;
    private bool _displayingDialogueNotif = false;
    private DialogueManager _dialogueManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] dialogueQueue; //0-empty, 1-?, 2-!
    [SerializeField] public Name npcName;

    private InventoryEnabler _uiEnabler;
    private Convo[] _dialogues;

    void Start()
    {
        _dialogueManager = DialogueManager.Get();
        bool didWork = DialogueInventory.GetDialogues(npcName, out _dialogues);
        if (!didWork)
        {
            Debug.LogError("Fuck");
        }
        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        // var uiEnableGo = GameObject.Find("Inventory Enabler");
        // if (uiEnableGo)
        // {
        //     _uiEnabler.GetComponent<InventoryEnabler>();
        // }
        // else
        // {
        //     Debug.LogError("No inventory enabled found!");
        // }
        

    }

    void Update()
    {
        if (_dialogueManager.isInDialogue)
        {
            return; //we don't need to do checks if we're in dialogue
        }
        double dist = Mathf.Sqrt(Mathf.Pow(_player.transform.position.x - transform.position.x, 2) + Mathf.Pow(_player.transform.position.z - transform.position.z, 2));
        double height = Mathf.Abs(_player.transform.position.y - transform.position.y);
        if (dist <= 50 && height <= 20 && 
            _dialogues.Length > 0 && !_dialogueManager.isInDialogue /*&& !_uiEnabler.GetCurrentUIState()*/)
        {
            _displayingDialogueNotif = true;
            int dialogueIndex = GetDialogue();
            if (Input.GetButtonDown("E"))
            {
                _dialogueManager.RunDialogue(this, in dialogueIndex); 
            }
        }
        else if ((dist > 70 || height > 20) && _displayingDialogueNotif)
        {
            _displayingDialogueNotif = false;
            spriteRenderer.sprite = dialogueQueue[0];
        }
    }
    
    // dialogue reference, index of dialogue in dialogues
    int GetDialogue()
    {
        int dialogueNum = 0;
        for (int i = 1; i < _dialogues.Length; i++)
        {
            var currentDialogue = _dialogues[i];
            bool containsNoGranted = true;
            foreach(var flag in currentDialogue.GrantedFlags)
            {
                if (_playerScript.dialogueFlags.Contains(flag))
                {
                    containsNoGranted = false;
                }
            }
            if(containsNoGranted)
            {
                var reqFlags = currentDialogue.RequiredFlags;
                dialogueNum = i;
                int numOfRequiredFlags = reqFlags.Length - 1;
                for (int reqIndex = 0; reqIndex < numOfRequiredFlags ; reqIndex++)
                {
                    if (!_playerScript.dialogueFlags.Contains(reqFlags[reqIndex]))
                    {
                        dialogueNum = 0;
                        break;
                        
                    }
                }
            }
        }

        if (dialogueNum == 0)
        {
            spriteRenderer.sprite = dialogueQueue[1];
            return 0; 
        }

        spriteRenderer.sprite = dialogueQueue[2];
        return dialogueNum;
    }

    public ref Convo GetConvo(in int index)
    {
        Debug.Log(index);
        return ref _dialogues[index];
    }
    
}
