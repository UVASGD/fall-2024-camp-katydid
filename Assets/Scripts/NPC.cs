using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using TMPro;
using TreeEditor;
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
        for (int i = 1; i < _dialogues.Length; i++)
        {
            var currentDialogue = _dialogues[i];
            var flags = currentDialogue.RequiredFlags;
            if (flags.Length < 1)
            {
                Debug.Log(npcName +" has no required flags for " + _dialogues[i].Dialogue);
            }
            var flagAddedOnComplete = flags.Last();
            
            if (!_playerScript.dialogueFlags.Contains(flagAddedOnComplete))
            {
                int numOfRequiredFlags = flags.Length - 1;
                for (int reqIndex = 0; reqIndex < numOfRequiredFlags ; reqIndex++)
                {
                    if (!_playerScript.dialogueFlags.Contains(flags[reqIndex]))
                    {
                        spriteRenderer.sprite = dialogueQueue[1];
                        return i-1;
                    }
                }
            }
        }
        spriteRenderer.sprite = dialogueQueue[2];
        return _dialogues.Length-1;
    }

    public ref Convo GetConvo(in int index)
    {
        if (index > _dialogues.Length)
        {
            Debug.Log("Warning: Too long");
        }
        
        return ref _dialogues[index];
    }
    
}
