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
    [SerializeField] private SpriteRenderer hairColor;
    [SerializeField] private SpriteRenderer eye;
    [SerializeField] private SpriteRenderer eyeColor;

    [SerializeField] private Sprite[] bodies;
    [SerializeField] private Sprite[] hairs;
    [SerializeField] private Sprite[] hairColors;
    [SerializeField] private Sprite[] eyes;
    [SerializeField] private Sprite[] eyeColors;

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
        int hairIndex = PlayerPrefs.GetInt("hairIndex");
        hair.sprite = hairs[hairIndex];
        hairColor.sprite = hairColors[(PlayerPrefs.GetInt("hairCIndex")) + hairIndex * 5];
        int eyeIndex = PlayerPrefs.GetInt("eyeIndex");
        eye.sprite = eyes[eyeIndex];
        eyeColor.sprite = eyeColors[(PlayerPrefs.GetInt("eyeCIndex")) + eyeIndex * 5];

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
