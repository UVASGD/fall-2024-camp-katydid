using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool moveLock = true;
    
    //private Vector2 _input;
    private CharacterController _characterController;
    private float speed = 50f;

    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private SpriteRenderer eyes;
    [SerializeField] private SpriteRenderer mouth;
    [SerializeField] private SpriteRenderer top;

    [SerializeField] private Sprite[] bodies;
    [SerializeField] private Sprite[] hairs;
    [SerializeField] private Sprite[] eyePairs;
    [SerializeField] private Sprite[] mouths;
    [SerializeField] private Sprite[] tops;

    public enum flags { defaultFlag, //flag put on all dialogue
            testFlag1, 
            testFlag2,
            testFlag3,
            testFlag4,
            testItemFlag//demo test flags
    };
    public HashSet<flags> dialogueFlags = new HashSet<flags>();

    private void Start()
    {
        body.sprite = bodies[PlayerPrefs.GetInt("bodyIndex")];
        hair.sprite = hairs[PlayerPrefs.GetInt("hairIndex")];
        eyes.sprite = eyePairs[PlayerPrefs.GetInt("eyeIndex")];
        mouth.sprite = mouths[PlayerPrefs.GetInt("mouthIndex")];
        top.sprite = tops[PlayerPrefs.GetInt("topIndex")];

        _characterController = GetComponent<CharacterController>();

        SaveData data = SaveSystem.LoadPlayer();

        for(int i=0; i < data.flags.Length; i++)
        {
            dialogueFlags.Add((flags)data.flags[i]);
        }
        
        transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        moveLock = true;
        Invoke("moveLockOff", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveLock)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _characterController.Move(move * Time.deltaTime * speed);
        }

        if (Input.GetButtonDown("esc"))
        {
            SaveSystem.SavePlayer(this);
        }
    }

    void moveLockOff()
    {
        moveLock = false;
    }

    //testing purposes
    public void printFlags()
    {
        flags[] flagsEnum = new Player.flags[dialogueFlags.Count];
        dialogueFlags.CopyTo(flagsEnum);
        string output = "";
        for (int i = 0; i < flagsEnum.Length; i++)
        {
            output += flagsEnum[i];
            output += " ";
        }
        Debug.Log(output);
    }
}
