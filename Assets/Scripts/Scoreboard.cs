using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Class that fills the scoreboard with the highscores 
public class Scoreboard : MonoBehaviour
{
    public GameObject cellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var players = DataSaver.loadData<Players>("players");
        for (int i = 0; i < players.players.Count; i++)
        {
            GameObject obj = Instantiate(cellPrefab);
            obj.transform.SetParent(this.gameObject.transform, false);
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text = players.players[i].name;
            obj.transform.GetChild(2).GetComponent<TMP_Text>().text = players.players[i].highScore.ToString();

        }
        
    }

}
