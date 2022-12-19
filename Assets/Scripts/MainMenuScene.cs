using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class that handles the Main Menu 
public class MainMenuScene : MonoBehaviour
{
    public TMP_Text welcomeText;


    void Start()
    {
        welcomeText.text = "Welcome " + ExchangeBetweenScenes.playerName + " !";
    }
}
