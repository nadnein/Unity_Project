using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Switch : MonoBehaviour
{

    public Image On;
    public Image Off;
    //[SerializeField] AudioSource backgroundMusic;
    private GameObject music;
    private AudioSource musicSource;
    int index;

    void Start()
    {
        music = GameObject.FindWithTag("GameMusic");
        musicSource = music.GetComponent<AudioSource>();
    }

    public void ON()
    {

        index = 1;
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
        musicSource.Stop();

    }

    public void OFF()
    {
        index = 0;
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
        musicSource.Play();
    }
}