using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
// ReSharper disable InconsistentNaming

public class DialogueInventory : MonoBehaviour
{
    

    
    
    /*
     * This system has three parts:
     * 1. AllDialogues have their Flags stripped out, with the text of the dialogues stored in _flattenedDialogues
     * 2. learnedDialogueStrings is a list of Strings in the order they were learned. This makes it easy to access
     * 3. _learnedDialogueMetadata has the data necessary to quickly save the dialogue in a space efficient manner
     * The existence of _singleton is not necessary, this could be done with static functions and objects,
     * and a boolean that indicated if the class is set up. However, I am comfortable with the Singleton pattern
     */
    
    private static SortedDictionary<Name, String[]> _flattenedDialogues;
    private static DialogueInventory _singleton;
    private Player _player;
    // LearnedDialogues holds all the necessary strings in one list of easy access,
    // and holds all the metadata in another list 
    public List<String> learnedDialogueStrings;
    private List<ConvoMetadata> _learnedDialogueMetadata;

        public static DialogueInventory Get()
    {
        SetupSingleton();
        return _singleton;
    }
   
    
    private static void SetupSingleton(Player inPlayer = null)
    {
        if (_singleton != null)
        {
            return;
        }

        _flattenedDialogues = new SortedDictionary<Name, String[]>();

        // setup flattened dialogue
        _flattenedDialogues.Clear();

        foreach (var npc in DialogueData.AllDialogues)
        {
            var strings = new List<string>();

            foreach (var convo in npc.Value)
            {
                strings.AddRange(convo.Dialogue);
            }
            _flattenedDialogues.Add(npc.Key, strings.ToArray());
        }
        
        if (inPlayer == null)
        {
            inPlayer = GameObject.Find("Player").GetComponent<Player>();
        }
        _singleton = inPlayer.AddComponent<DialogueInventory>();
        _singleton._player = inPlayer;
        _singleton.learnedDialogueStrings = new List<String>();
        _singleton._learnedDialogueMetadata = new List<ConvoMetadata>();
    }
    
    public static bool GetDialogues(Name inName, out Convo[] npcConvos)
    {
        return DialogueData.AllDialogues.TryGetValue(inName, out npcConvos);
    }
    
    // the metadata lets us turn the strings easily into saved references
    public static void MarkDialogueDisplayed(Name inName, int index)
    {
        SetupSingleton();
        _singleton.learnedDialogueStrings.Add(_flattenedDialogues[inName][index]);
        _singleton._learnedDialogueMetadata.Add(new ConvoMetadata(inName, index));
    }
    
    //called in SavePlayer, before saving player data. Stores the data in the player's knownDialoguesSaveLocation var
    public static void SaveData() 
    {
        SetupSingleton();
        _singleton._player.GetComponent<Player>().SaveLearnedDialogues(_singleton._learnedDialogueMetadata);
    }
    
    // turns loaded tuple array into string array
    // stores loaded tuple array as metadata
    public static void LoadData(Player player, in List<ConvoMetadata> data)
    {
        SetupSingleton(player);
        _singleton.learnedDialogueStrings.Clear();
        _singleton._learnedDialogueMetadata.Clear();
        foreach (var metadata in data)
        {
            _singleton._learnedDialogueMetadata.Add(metadata);
            _singleton.learnedDialogueStrings.Add(_flattenedDialogues[metadata.npcName][metadata.dialogueIndex]);
        }
    }
    
    public static DialogueInventory Get()
    {
        SetupSingleton();
        return _singleton;
    }
}