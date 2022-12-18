using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    public TMP_Text welcomeText;


    void Start()
    {
        welcomeText.text = "Welcome " + ExchangeBetweenScenes.playerName + " !";
    }


    public void StartGame()
    {
        SceneManager.LoadScene("FarmAnimals");
    }
}
