using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProfile : MonoBehaviour
{
    // a game profile.

    private string _name;
    private List<int> _scores;

    public GameProfile(string name)
    {
        _name = name;
    }

    // Finds the best score. 
    public int FindBestScore()
    {
        int currentBestScore = 0;
        for (int i = 0; i < _scores.Count; i++)
        {

        }
        return 0; //dummy for now
    }

    public List<int> GetScores()
    {
        return _scores;
    }
    /*
        public void SetName(string name){
            if (name)
        }
    */



}
