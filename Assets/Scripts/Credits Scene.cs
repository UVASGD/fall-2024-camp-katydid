using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{ 
    public void BackToStart()
    {
        SceneManager.LoadScene(sceneName: "StartMenu");
    }
}