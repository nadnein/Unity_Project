using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class SceneLoader : MonoBehaviour
{
    // Class to switch between scenes 

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadQuitScene()
    {
        SceneManager.LoadScene("QuitScene");
    }

    public void LoadMenuScene(string playerName)
    {
        SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
        //Players players = DataSaver.loadData<Players>("players");
        //Player player = players.GetPlayerByName(playerName);
        //TMP_Text welcomeText = GameObject.Find("WelcomeText").GetComponent<TMP_Text>();
        //Debug.Log(welcomeText.text);
        //welcomeText.text = "WELCOME";

    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }



    //TODO: also add a quitgame x in the corner. 


}
