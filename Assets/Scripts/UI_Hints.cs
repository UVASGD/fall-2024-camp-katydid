using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hints : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;

    public GameObject hintUI;
    // Start is called before the first frame update

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        PopulateGrid();
    }
    void PopulateGrid()
    {
        Debug.Log("started");
        foreach (Player.flags flag in playerScript.dialogueFlags) {
            if (Player.flags.defaultFlag == flag) {
                // is a text dialogue
                Debug.Log("Created");
                GameObject UIPiece = Instantiate(hintUI,transform);
                UIPiece.GetComponent<UI_HintLabel>().setName(flag);
                GameObject UIPiece1 = Instantiate(hintUI, transform);
                GameObject UIPiece2 = Instantiate(hintUI, transform);
                GameObject UIPiece3 = Instantiate(hintUI, transform);
                // do ui stuff in here
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
