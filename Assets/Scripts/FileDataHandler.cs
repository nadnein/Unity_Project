using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class FileDataHandler : MonoBehaviour
{
    // Handler of the saving and loading data to file. 

    public string _filename;

    public FileDataHandler(string filename)
    {
        _filename = filename;

    }

    public void WriteToFile(GameProfile profile)
    {
        string path = this.GetFilePath(_filename);

        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            string stringToWrite = JsonUtility.ToJson(profile);
            writer.Write(stringToWrite);
        }
        Debug.Log("Wrote to file.");
    }


    public GameProfile ReadFromFile()
    {
        string path = this.GetFilePath(_filename);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string stringFromFile = reader.ReadToEnd();
                GameProfile profile = JsonUtility.FromJson<GameProfile>(stringFromFile);
                Debug.Log("Done reading from file.");
                return profile;
            }
        }
        else
        {
            Debug.Log("Files does not exist.");
            return null;
        }
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    /*
        public void LoadGame()
        {
            var input = gameObject.GetComponent<InputField>();
            GameProfile newGameProfile = new GameProfile(input);
            input.onEndEdit.AddListener(new GameProfile(input));

        }
        */

}

