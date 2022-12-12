using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;


using UnityEngine;

public class FileDataHandler : MonoBehaviour
{
    // Handler of the saving and loading data to file. 

    public string _filename = "test.json";

    public TMP_InputField _nameInput;


    public List<GameProfile> players;


    public FileDataHandler(string filename)
    {
        _filename = filename;

    }


    public void WriteToFile(GameProfile profile)
    {
        string path = this.GetFilePath(_filename); //Application.dataPath;
        FileStream fileStream = null;
        Debug.Log(path);

        if (!File.Exists(path))
        {
            fileStream = new FileStream(path, FileMode.Create);
            Debug.Log("New file created.");

        }
        else
        {
            fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
            Debug.Log("Try to write to already existing file");
        }
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            string stringToWrite = JsonUtility.ToJson(profile);
            writer.Write(stringToWrite);
            Debug.Log("Wrote to file.");
        }
    }



    public void ReadFromFile()
    {
        string path = this.GetFilePath(_filename);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string stringFromFile = reader.ReadToEnd();
                GameProfile profile = JsonUtility.FromJson<GameProfile>(stringFromFile);
                players.Add(profile);
                Debug.Log("Done reading from file.");
            }
        }
        else
        {
            Debug.Log("Files does not exist.");
        }
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName; //"/Users/jinbakketun/Unity_Project/Assets/Scripts";
    }

    /*
        public void LoadGame()
        {
            var input = gameObject.GetComponent<InputField>();
            GameProfile newGameProfile = new GameProfile(input);
            input.onEndEdit.AddListener(new GameProfile(input));

        }
        */


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && !(_nameInput.Equals("")))
        {
            Debug.Log(_nameInput.text);

            //SceneLoader.Invoke("LoadMenuScene", 1f);
            GameProfile gp = new GameProfile(_nameInput.text);
            gp.addScore(10);
            gp.addScore(111);
            Debug.Log("The new objects name: " + gp.GetName());
            WriteToFile(gp);
        }
        else
        {
            // TODO: if player chooses a saved profile --> LoadGame()
        }

    }
    /*
        public void InitialisePlayer()
        {
            // check if inputfield is not empty and enter. 

        }
    */
}

