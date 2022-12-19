using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

// Class to switch between scenes 

public class SceneLoader : MonoBehaviour
{

    public void LoadFarmAnimalGame()
    {
        SceneManager.LoadScene("FarmAnimals");
    }
    public void LoadJungleAnimalGame()
    {
        SceneManager.LoadScene("JungleAnimals");
    }
    public void LoadSafariAnimalGame()
    {
        SceneManager.LoadScene("SafariAnimals");
    }

    public void LoadSelectGameModeScene()
    {
        SceneManager.LoadScene("SelectGameModeScene");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }
    public void LoadStatisticsScene()
    {
        SceneManager.LoadScene("Statistics");
    }

    public void LoadScoreboardScene()
    {
        SceneManager.LoadScene("Scoreboard");
    }





}
