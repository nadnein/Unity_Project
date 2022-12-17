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

    public void LoadFarmAnimalGame()
    {
        SceneManager.LoadScene("FarmAnimals");
    }

    public void LoadQuitScene()
    {
        SceneManager.LoadScene("QuitScene");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");

    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }



    //TODO: also add a quitgame x in the corner. 


}
