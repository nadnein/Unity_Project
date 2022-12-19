using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


[System.Serializable]
public class Player

{
    // a game profile.
    //[SerializeField] private string _name;
    public string name;
    [SerializeField] private List<int> _scores;
    public int highScore = 0;

    public Player(string name)
    {
        //SetName(name);
        this.name = name;
        _scores = new List<int>();
    }

    // Finds the best score. 
    public int FindBestScore()
    {
        int currentBestScore = 0;
        for (int i = 0; i < _scores.Count; i++)
        {
            if (_scores[i] > currentBestScore)
            {
                currentBestScore = _scores[i];
            }

        }
        return currentBestScore;
    }

    public List<int> GetScores()
    {
        return _scores;
    }

    public void SetName(string name)
    {
        if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
        {
            this.name = name;
        }
        else
        {
            Debug.Log("Not valid name");
        }
    }

    public string GetName()
    {
        return name;
    }


}
