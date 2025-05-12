using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    private AudioSource _AudioSourceMenu;
    private AudioSource _AudioSourceGame;
    [SerializeField] AudioSource buttonSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var gameMusic = transform.GetChild(0).GetComponent<AudioSource>();
        _AudioSourceMenu = GetComponent<AudioSource>();
        if(gameMusic != null)
            _AudioSourceGame = gameMusic.GetComponent<AudioSource>();
    }

    public void ChangeSound(int index)
    {
        if (index == 0)
        {
            _AudioSourceGame.Stop();
            _AudioSourceMenu.Play();
        }
        else if (index == 1)
        {
            _AudioSourceMenu.Stop();
            _AudioSourceGame.Play();
        }
    }

    public void ButtonSound()
    {
        buttonSource.Play();
    }
}
