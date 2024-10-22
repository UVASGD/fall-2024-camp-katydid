using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_HintLabel : MonoBehaviour
{
    public TextMeshProUGUI name_text;
    public Image npc_avatar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setName(string name) {
        name_text.text = name;
    }
    public void setImage(Sprite avatar) {
        npc_avatar.overrideSprite = avatar;
    }
}
