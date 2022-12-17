using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DDTarget : MonoBehaviour
{
    // Contains code referring to the drag & drop targets 

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _completeClip;
    private AudioClip _animalSoundClip;

    private float _targetScale = 0.8f;
    private float _shrinkSpeed = 5f;

    public void SetAudioClip(AudioClip clip)
    {
        _animalSoundClip = clip;
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    }

    public void StartAudio()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _animalSoundClip;
        _audioSource.loop = true;
        _audioSource.PlayDelayed(0.5f);
    }

    private void Start()
    {
        StartAudio();
        //_audioSource.Play();    
    }


    public void ShowSoulutionAnimal()
    {
        _audioSource.PlayOneShot(_completeClip);
        while (transform.localScale.x < _targetScale)
        {
            transform.localScale += Vector3.one * Time.deltaTime * _shrinkSpeed;
        }

    }



    
}
