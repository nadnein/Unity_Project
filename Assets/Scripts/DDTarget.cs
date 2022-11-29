using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DDTarget : MonoBehaviour
{
    // Contains code referring to the drag & drop targets 

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _completeClip;

    private float _targetScale = 0.8f;
    private float _shrinkSpeed = 5f;

    public void SetAudioClip(AudioClip clip)
    {
        _completeClip = clip;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _completeClip;
        _audioSource.loop = true;
        _audioSource.PlayDelayed(1);
        //_audioSource.Play();    
    }


    public void Increase()
    {
        _audioSource.PlayOneShot(_completeClip);
        while (transform.localScale.x < _targetScale)
        {
            transform.localScale += Vector3.one * Time.deltaTime * _shrinkSpeed;
        }

    }



    
}
