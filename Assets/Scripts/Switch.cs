using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Switch : MonoBehaviour
{

    public Image On;
    public Image Off;
    [SerializeField] AudioSource backgroundMusic;
    int index;


    void Start()
    {

                
    }

    void Update()
    {
    
    }

    public void ON()
    {

        index = 1;
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
        backgroundMusic.Stop();

    }

    public void OFF()
    {
        index = 0;
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
        backgroundMusic.Play();
    }
}