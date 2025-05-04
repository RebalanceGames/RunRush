using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSes : MonoBehaviour
{
    private static GameObject instance;

    public AudioSource Ses;

    private void Start()
    {
        Ses = GetComponent<AudioSource>();
        
        if (Ses != null)
            Ses.volume = PlayerPrefs.GetFloat("MenuSes");
        else
            Debug.LogWarning("MenuSes: AudioSource atanmamış!");

        DontDestroyOnLoad(gameObject);

       if (instance == null)
            instance = gameObject;
       else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Ses != null)
           Ses.volume = PlayerPrefs.GetFloat("MenuSes");
    }
}