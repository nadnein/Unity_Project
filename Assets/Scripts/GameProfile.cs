using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


[System.Serializable]
public class GameProfile
{
    // a game profile.
    [SerializeField] private string _name;
    [SerializeField] private List<int> _scores;

    public GameProfile(string name)
    {
        SetName(name);
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
            _name = name;
        }
        else
        {
            Debug.Log("Not valid name");
        }
    }

    public string GetName()
    {
        return _name;
    }

    public void addReactionTime(float time)
    {


    }

    public void addScore(int score)
    {
        _scores.Add(score);
    }

}
