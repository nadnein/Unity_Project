using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Class that handels the background music
public class PlayMusic : MonoBehaviour
{
    private GameObject music;
    private AudioSource musicSource;


    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindWithTag("GameMusic");
        musicSource = music.GetComponent<AudioSource>(); 
        var currScene = SceneManager.GetActiveScene().name;
        if (currScene == "FarmAnimals" || currScene == "JungleAnimals" || currScene == "SafariAnimals")
        {
            musicSource.Stop();
        }
        else if (ExchangeBetweenScenes.musicStatus == "on" && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

}
