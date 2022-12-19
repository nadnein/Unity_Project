using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Class that stores a player object 
[System.Serializable]
public class Player

{
    public string name;
    [SerializeField] private List<int> _scores;
    public float highScore = 0;

    public Player(string name)
    {
        this.name = name;
        _scores = new List<int>();
    }

    public List<int> GetScores()
    {
        return _scores;
    }

    public string GetName()
    {
        return name;
    }

    public void addScore(int score)
    {
        _scores.Add(score);
    }

}
