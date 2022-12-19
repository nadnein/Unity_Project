using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that stores players in a list 

[Serializable]
public class Players
{
    public List<Player> players = new List<Player>();
        

    // returs a player by name 
    public Player GetPlayerByName(string name)
    {
        var player = players.Where(player => player.GetName() == name).FirstOrDefault();
        return player;
    }

    // deletes a player by name 
    public void DeletePlayer(string name)
    {
        int myIndex = this.players.FindIndex(player => player.name == name);
        int cnt = players.Count;
        
        Debug.Log("players ist so lang: " + cnt.ToString());
        foreach (var player in players)
        {
            Debug.Log(player.name);
        }

        Debug.Log(myIndex);
        this.players.RemoveAt(myIndex);
    }
}
