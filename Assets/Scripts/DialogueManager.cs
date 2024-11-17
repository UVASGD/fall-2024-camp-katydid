using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



public class DialogueManager : MonoBehaviour
{
    
    private GameObject player;
    private Player playerScript;
    public bool isInDialogue = false;

    [SerializeField] private InventoryItem[] inventoryItem;
    [SerializeField] private Camera camera;
    private Canvas _textbox;
    private TextMeshProUGUI _textMeshPro;
    private TextMeshProUGUI _nameTMP;
    private float _minXBoundary; // Minimum x boundary in screen space
    private float _maxXBoundary; // Maximum x boundary in screen space
    private RectTransform _arrowRectTransform;
    
    private int dialogueIndex = 0;
    private int currentConvoIndex = 0;
    public NPC currentNPC;

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
        _textbox = GameObject.Find("Dialogue - Canvas").GetComponent<Canvas>();
        var tmps = _textbox.GetComponentsInChildren<TextMeshProUGUI>();
        _textMeshPro = tmps[0];
        _nameTMP = tmps[1];
        GameObject arrowRect = GameObject.Find("TextBubbleArrow");
        if (arrowRect)
        {
            _arrowRectTransform = arrowRect.GetComponent<RectTransform>();
            _arrowRectTransform.gameObject.SetActive(false);
        }
        else
        {
            _arrowRectTransform = new RectTransform();
        }
        _textbox.enabled = false;
    }

    void Update()
    {
        if ((Input.GetButtonDown("Space") || Input.GetButtonDown("E")) && isInDialogue)
        {
            NextDialogue();
        }
    }
    
    void AdjustDialogueArrow()
    {
        Vector3 npcPosition = transform.position;
        Vector3 npcScreenPosition = camera.WorldToScreenPoint(npcPosition);
                
                if (npcScreenPosition.x > _minXBoundary && npcScreenPosition.x < _maxXBoundary)
                {
                    _arrowRectTransform.anchoredPosition = new UnityEngine.Vector2(npcScreenPosition.x, Screen.height * .60f);

                } else if (npcScreenPosition.x < _minXBoundary)
                {
                    _arrowRectTransform.anchoredPosition = new UnityEngine.Vector2(_minXBoundary, Screen.height * .60f);

                }  else if (npcScreenPosition.x > _maxXBoundary)
                {
                    _arrowRectTransform.anchoredPosition = new UnityEngine.Vector2(_maxXBoundary, Screen.height * .60f);
                }
    }

    
    public void RunDialogue(in NPC npc, in int convoIndex)
    {
        //AdjustDialogueArrow();
        _nameTMP.SetText(NameEnumToString(npc.npcName));
        
        playerScript.moveLock = true;
        dialogueIndex = 0;
        currentConvoIndex = convoIndex;
        currentNPC = npc;
        dialogueIndex = 0;
        _textbox.enabled = true;
        isInDialogue = true;
        _textMeshPro.SetText(currentNPC.GetConvo(currentConvoIndex).Dialogue[dialogueIndex]);
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
            _textbox.enabled = false;

            foreach (var itemType in currentNPC.GetConvo(currentConvoIndex).GrantedItems)
            {
                if (itemType != InventoryItem.ItemType.None)
                {
                    playerScript.AddToInventory(new InventoryItem(itemType));
                }
            }

            currentNPC.GetConvo(currentConvoIndex).GrantedItems = new[] { InventoryItem.ItemType.None };
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
    
    public static string NameEnumToString(Name name)
    {
        var names = new string[]
        {
            "Ed", "Timothy", "Janet", "Alex", "Dylan", "Earl",
            "Nate", "Charles", "Gen", "Mallory", "Cletus", "Dawn", "Tony", "Dennis",
            "Benny", "Steph", "Drew", "Kyle", "Kylie", "Vanessa", "Coach", "April", "Kai", "Kraken", "Death"
        };
        return names[(int)name];
    }
    
}
