using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;


using UnityEngine;

using UnityEngine.UI;

public class FileDataHandler : MonoBehaviour
{
    // Handler of the saving and loading data to file. 

    public TMP_InputField _nameInput;

    private List<Player> _players;


    // Saves a players name and score to a file.  
    public void WriteToFile(Player profile)
    {
        // creates a directory
        if (!Directory.Exists(Application.persistentDataPath + "/Profiles/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Profiles/");
        }

        // the path to the file.
        string path = Path.Combine(Application.persistentDataPath, "Profiles/test.json");
        FileStream fileStream = null;

        // file is created if no such file. 
        if (!File.Exists(path))
        {
            fileStream = new FileStream(path, FileMode.Create);
            Debug.Log("New file created.");

        }
        // write to existing file. 
        else
        {
            fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
            Debug.Log("write to already existing file");
        }
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            // writes the string in json format. 
            string stringToWrite = JsonUtility.ToJson(profile) + "\n";
            writer.Write(stringToWrite);
            Debug.Log("Wrote to file.");
        }
    }

    public void ReadFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "Profiles/test.json");

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    Player profile = JsonUtility.FromJson<Player>(str);
                    _players.Add(profile);
                }
                Debug.Log("Done reading from file.");
            }
        }
        else
        {
            Debug.Log("Files does not exist.");
        }
    }

    void Update()
    {
        // checks if someone clicks the inputfield. 
        if (Input.GetKeyUp(KeyCode.Return) && !(_nameInput.Equals("")))
        {
            // SceneLoader.Invoke("LoadMenuScene", 1f);
            Player gp = new Player(_nameInput.text);
            WriteToFile(gp);
        }
        else
        {
            // TODO: if player chooses a saved profile. 
            // SceneLoader.Invoke("LoadMenuScene", 1f);
        }

    }


    public List<Player> GetGameProfiles()
    {
        return _players;
    }
}

