using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hints : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Player.flags flag in playerScript.dialogueFlags) {
            if (Player.flags.defaultFlag == flag) {
                // is a text dialogue
                Debug.Log(flag);
            } else
            {
                // other cases, maybe items or whatever
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
