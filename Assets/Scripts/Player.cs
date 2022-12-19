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
    [SerializeField] public List<int> _scores;
    // [SerializeField] public Dictionary<int, float> _scores;

    public int highScore = 0;

    public Player(string name)
    {
        //SetName(name);
        this.name = name;
        _scores = new List<int>();
        // _scores = new Dictionary<int, float>();
    }

}
