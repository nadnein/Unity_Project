using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoardScene : MonoBehaviour
{

    public GameObject tableContainer, tableChild;
    public SceneLoader loader;
    public Players players;
    // Start is called before the first frame update


    void Start()
    {

        try
        {
            //DataSaver.deleteData("players");
            players = DataSaver.loadData<Players>("players");
            Debug.Log(players);
            Debug.Log(players.players.Count);

            foreach (var player in players.players)
            {
                addElementToScoreBoard(player);
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

    // Update is called once per frame
    void Update()
    {
    }


    public void addElementToScoreBoard(Player player)
    {
        if (tableContainer.transform.childCount < 10)
        {
            GameObject newEntry = Instantiate(tableChild, tableContainer.transform);
            var nameText = newEntry.GetComponentInChildren<TMP_Text>();
            var dateText = newEntry.GetComponentInChildren<TMP_Text>();
            var scoreText = newEntry.GetComponentInChildren<TMP_Text>();

            nameText.text = "Navn";  //player.GetName;
            dateText.text = "date";
            scoreText.text = "120"; //player.FindBestScore.ToString //bestScore.ToString;
        }

    }
}
