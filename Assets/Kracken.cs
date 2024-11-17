using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kracken : MonoBehaviour
{

    private DialogueManager _dialogueManager;
    [SerializeField] GameObject krackenObject;

    void Start()
    {
        _dialogueManager = DialogueManager.Get();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dialogueManager.isInDialogue && _dialogueManager.currentNPC.npcName == Name.Kraken)
        {
            krackenObject.SetActive(true);
        }
        else
        {
            krackenObject.SetActive(false);
        }
    }
}
