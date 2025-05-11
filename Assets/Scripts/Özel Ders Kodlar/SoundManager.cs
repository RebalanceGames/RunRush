using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource _AudioSource;
    public AudioClip[] sesler;
    AudioSource buttonSource;
    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        buttonSource = GetComponentInChildren<AudioSource>();
    }

    public void ChangeSound(int index)
    {
        _AudioSource.clip = sesler[index];
        _AudioSource.Play();
    }

    public void ButtonSound()
    {
        buttonSource.Play();
    }
}
