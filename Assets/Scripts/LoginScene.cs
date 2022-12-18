
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LoginScene : MonoBehaviour
{
    public TMP_InputField inputField;
<<<<<<< HEAD
    public GameObject keyboard, tableContainer, tableChild;
=======
    public GameObject keyboard, tableContainer, tableChild;  
>>>>>>> jin_new
    public SceneLoader loader;
    public Players players;


    private void Start()
    {
        keyboard.SetActive(false);

        try // open file with players if it exists
        {
            players = DataSaver.loadData<Players>("players");

            // load button for each player in file 
            foreach (var player in players.players)
            {
                addProfileButton(player.name);
            }
        }
        catch // otherwise create new player file 
        {
            players = new Players();
            DataSaver.saveData(players, "players");

        }

    }



    public void openKeyboard()
    {
        keyboard.SetActive(true);
    }

    public void closeKeyboard()
    {
        keyboard.SetActive(false);
    }

    // writes text to the input field 
    public void useKeyboard(string letter)
    {
        inputField.text += letter;
    }

    // deletes either selected (highlighted) text or last letter from text 
    public void deleteSelection()
    {
        var start = inputField.selectionAnchorPosition;
        var end = inputField.selectionFocusPosition;

        if (end - start > 0)
        {
            string selectedText = inputField.text.Substring(Mathf.Min(start, end), Mathf.Abs(end - start));
            inputField.text = inputField.text.Remove(start, end - start);
        }
        else
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
        }

    }

    public void addProfileButton(string playerName)
    {

        if (tableContainer.transform.childCount < 4) // only add new profile if less than 4 in table 
        {
            // init new profile gameobject to scene with name and delete button 
            GameObject newProfile = Instantiate(tableChild, tableContainer.transform);

            var nameButton = newProfile.transform.GetChild(0);
            var deleteButton = newProfile.transform.GetChild(1);

            // add listener to delete button -> function that deletes player profile 
            deleteButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Destroy(newProfile); // destroy gameobject
                players = DataSaver.loadData<Players>("players");
                players.DeletePlayer(playerName); // delete player from player object 
                DataSaver.saveData(players, "players"); // save modified list of players to file 
            });

            // change name of profile button prefab to player name 
            var buttonText = nameButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = playerName;

            players = DataSaver.loadData<Players>("players");
            var player = players.GetPlayerByName(playerName);

            if (player == null) // save new player to list of players if not already there
            {
                player = new Player(playerName);
                players.players.Add(player);
                DataSaver.saveData(players, "players");
            };


            // add listener to name button -> function that deletes player profile 
            nameButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                // safe player name to static class that can be accessed from all scenes 
                ExchangeBetweenScenes.playerName = playerName;

                // load the next Scene 
                SceneManager.LoadScene("MainMenuScene");
            });

        }
    }

    public void addNewProfile()
    {
        string playerName = inputField.text;
        addProfileButton(playerName);
    }


}


