using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        else if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

}
