
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
    public GameObject keyboard, tableContainer, tableChild;
    public SceneLoader loader;
    public Players players;


    private void Start()
    {
        keyboard.SetActive(false);

        try
        {
            //DataSaver.deleteData("players");
            players = DataSaver.loadData<Players>("players");
            Debug.Log(players.players.Count);
            foreach (var player in players.players)
            {
                Debug.Log(player.name);
                addProfileButton(player.name);
            }
        }
        catch
        {
            Console.WriteLine("There was an error!");
        }
        finally
        {
            players = new Players();
            DataSaver.saveData(players, "players");

        }
    }

    private void Awake()
    {
        players = DataSaver.loadData<Players>("players");

    }


    public void openKeyboard()
    {
        keyboard.SetActive(true);
    }

    public void closeKeyboard()
    {
        keyboard.SetActive(false);
    }


    public void useKeyboard(string letter)
    {
        inputField.text += letter;
    }

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

        if (tableContainer.transform.childCount < 4)
        {
            GameObject newProfile = Instantiate(tableChild, tableContainer.transform);
            var nameButton = newProfile.transform.GetChild(0);

            var deleteButton = newProfile.transform.GetChild(1);
            deleteButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Destroy(newProfile);
                Debug.Log("players size loginscene" + players.players.Count);
                players = DataSaver.loadData<Players>("players");
                players.DeletePlayer(playerName);
                DataSaver.saveData(players, "players");
            });

            // change Name of Profile Button Prefab 
            var buttonText = nameButton.GetComponentInChildren<TMP_Text>();

            buttonText.text = playerName;

            nameButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                ExchangeBetweenScenes.playerName = playerName;
                players = DataSaver.loadData<Players>("players");
                var player = players.GetPlayerByName(playerName);

                if (player == null) // save new player to list of players if not already there
                {
                    player = new Player(playerName);
                    Debug.Log("bei NameButton: " + players.players.Count);
                    players.players.Add(player);
                    Debug.Log(players.players[0].GetName());
                    DataSaver.saveData(players, "players");
                };

                // Load the next Scene 
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


