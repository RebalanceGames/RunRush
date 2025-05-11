using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrtakManager : MonoBehaviour
{
    public static OrtakManager Instance;
    public GameObject[] Canvas;
    public static bool oyunBasladimi;
    
    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu()
    {
        oyunBasladimi = false;
        Canvas[0].SetActive(true);
        Canvas[1].SetActive(false);
    }
    public void OpenGame()
    {
        oyunBasladimi = true;
        Canvas[0].SetActive(false);
        Canvas[1].SetActive(true);
    }

    public void MessageBox(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0;
    }

    /*public void ResumeGame(GameObject obj)
    {
        obj.SetActive(false);
        Time.timeScale = 1;
    }*/
}
