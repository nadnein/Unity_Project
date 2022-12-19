using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Class that handles switch between on and off button of background music 
public class Switch : MonoBehaviour
{
    public Image On;
    public Image Off;
    private GameObject music;
    private AudioSource musicSource;

    void Start()
    {
        music = GameObject.FindWithTag("GameMusic");
        musicSource = music.GetComponent<AudioSource>();
        if(musicSource.isPlaying)
        {
            On.gameObject.SetActive(true);
            Off.gameObject.SetActive(false);
        }
        else
        {
            musicSource.Stop();
            Off.gameObject.SetActive(true);
            On.gameObject.SetActive(false);
        }
    }

    public void ON()
    {

        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
        musicSource.Stop();
        ExchangeBetweenScenes.musicStatus = "off";

    }

    public void OFF()
    {
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
        musicSource.Play();
        ExchangeBetweenScenes.musicStatus = "on";

    }
}