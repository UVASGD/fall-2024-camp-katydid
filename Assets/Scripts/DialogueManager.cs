using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



public class DialogueManager : MonoBehaviour
{
    /*public struct Dialogue
    {
        Dialogue(string name, string[] dialogueText, Flag[] requiredFlags, Flag[] grantedFlags, 
            InventoryItem.ItemType[] grantedItems
        ) { 
            Name = name;
            DialogueText = dialogueText;
            RequiredFlags = requiredFlags;
            GrantedFlags = grantedFlags;
            GrantedItems = grantedItems;
        }

        Dialogue(string name);
        
        public string Name;
        public string[] DialogueText;
        public Flag[] RequiredFlags;
        public Flag[] GrantedFlags;
        public InventoryItem.ItemType[] GrantedItems;
    }*/
    
    private GameObject player;
    private Player playerScript;
    public bool isInDialogue = false;

    [SerializeField] private InventoryItem[] inventoryItem;
    [SerializeField] private new Camera camera;
    private Canvas _textbox;
    private TextMeshProUGUI _textMeshPro;
    private float _minXBoundary; // Minimum x boundary in screen space
    private float _maxXBoundary; // Maximum x boundary in screen space
    //private RectTransform arrowRectTransform;
    
    private int dialogueIndex = 0;
    private int currentConvoIndex = 0;
    private NPC currentNPC;

    private static DialogueManager _singleton;
    
    public static DialogueManager Get()
    {
        if (_singleton == null)
        {
            GameObject empty = new GameObject("Dialogue Manager");
            _singleton = empty.AddComponent<DialogueManager>();
        }
        return _singleton;
    }

    void Start()
    {
        _minXBoundary = Screen.width * 0.25f;
        _maxXBoundary = Screen.width * 0.75f;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        //arrowRectTransform = GameObject.Find("TextBubbleArrow").GetComponent<RectTransform>();
        _textbox = GameObject.Find("Dialogue - Canvas").GetComponent<Canvas>();
        _textMeshPro = _textbox.GetComponentInChildren<TextMeshProUGUI>();
        _textbox.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Space") && isInDialogue)
        {
            NextDialogue();
        }
    }
    
    /*void AdjustDialogueArrow()
    {
        UnityEngine.Vector3 npcPosition = transform.position;
        UnityEngine.Vector3 npcScreenPosition = camera.WorldToScreenPoint(npcPosition);
                
                if (npcScreenPosition.x > _minXBoundary && npcScreenPosition.x < _maxXBoundary)
                {
                    arrowRectTransform.anchoredPosition = new UnityEngine.Vector2(npcScreenPosition.x, Screen.height * .60f);

                } else if (npcScreenPosition.x < _minXBoundary)
                {
                    arrowRectTransform.anchoredPosition = new UnityEngine.Vector2(_minXBoundary, Screen.height * .60f);

                }  else if (npcScreenPosition.x > _maxXBoundary)
                {
                    arrowRectTransform.anchoredPosition = new UnityEngine.Vector2(_maxXBoundary, Screen.height * .60f);
                }
    }*/

    
    public void RunDialogue(in NPC npc, in int convoIndex)
    {
        if (!npc)
        {
            //InnerMonologue
        }
        //AdjustDialogueArrow();
        isInDialogue = true;
        playerScript.moveLock = true;
        currentConvoIndex = convoIndex;
        currentNPC = npc;
        NextDialogue();
    }
    
    private void NextDialogue()
    {
        Convo currentConvo = currentNPC.GetConvo(currentConvoIndex);
        if(dialogueIndex < currentConvo.Dialogue.Length)
        {
            _textbox.enabled = true;
            _textMeshPro.SetText(currentConvo.Dialogue[dialogueIndex]);
            dialogueIndex++;
        }
        else
        {
            dialogueIndex = 0;
            isInDialogue = false;
            playerScript.moveLock = false;
            if (currentConvoIndex != 0)
            {
                foreach (var flag in currentConvo.GrantedFlags)
                {
                    playerScript.dialogueFlags.Add(flag);
                    //imScript.monologueCheck(reqs[currentConvoIndex].Last());
                }
            }
        }
    }
}
