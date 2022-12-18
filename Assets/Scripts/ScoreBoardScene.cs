using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreBoardScene : MonoBehaviour
{

    public GameObject tableContainer, tableChild;
    public Players players;


    void Start()
    {
        try
        {
            players = DataSaver.loadData<Players>("players");
            IEnumerable<Player> sortedPlayers = players.players.OrderBy(player => -player.highScore);
            Debug.Log(sortedPlayers);
            foreach (var player in sortedPlayers)
            {

                addElementToScoreBoard(player);
            }
        }
        catch
        {
            players = new Players();
            DataSaver.saveData(players, "players");
        }
    }


    public void addElementToScoreBoard(Player player)
    {
        if (tableContainer.transform.childCount < 10)
        {
            GameObject newEntry = Instantiate(tableChild, tableContainer.transform);

            var dateText = newEntry.transform.GetChild(0);
            var nameText = newEntry.transform.GetChild(1);
            var scoreText = newEntry.transform.GetChild(2);

            dateText.GetComponentInChildren<TMP_Text>().text = "20.02";
            nameText.GetComponentInChildren<TMP_Text>().text = player.name;
            scoreText.GetComponentInChildren<TMP_Text>().text = player.highScore.ToString();
        }

    }
}

