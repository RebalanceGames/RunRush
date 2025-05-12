using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ayberk;
using Random = UnityEngine.Random;

public class LevelEditor : MonoBehaviour
{
    private BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    
    [SerializeField] private GameObject karakter;
    [SerializeField] private Vector3 karakterStartPoint;

    [SerializeField] private GameObject balyoz; // 5
    [SerializeField] private GameObject balyozOrta; // 10
    [SerializeField] private GameObject donenIgne; // 1

    [SerializeField] private GameObject[] spawnPoints;
    
   

    private void Start()
    {
        
        int level = _BellekYonetim.VeriOku_i("SonLevel");
        if (level > 4)
        {
            int kalan = level % 5;
            level -= kalan;
            print("level : " + level + "kalan :" + kalan);
            int hesaplama = level * 2;
            
            print((hesaplama));
            if(hesaplama < 60)
                karakter.transform.position = new Vector3(karakterStartPoint.x, karakterStartPoint.y, -30 - hesaplama);

            if (level >= 5)
            {
                int spawnPointCount = Random.Range(0, 2);
                int randomdeger = Random.Range(0, 2);
                Instantiate(balyoz, spawnPoints[randomdeger].transform.position, Quaternion.identity);
                if (hesaplama == -50)
                {
                    spawnPointCount = Random.Range(0, 2);
                    int randomdeger2 = Random.Range(0, 4);
                    Instantiate(balyoz, spawnPoints[randomdeger2].transform.position, Quaternion.identity);
                }
                else if (hesaplama == -60)
                {
                    spawnPointCount = Random.Range(0, 2);
                    int randomdeger3 = Random.Range(0, 12);
                    Instantiate(balyoz, spawnPoints[randomdeger3].transform.position, Quaternion.identity);
                }
            }

            if (level >= 10)
            {
                
            }

            if (level >= 15)
            {
                
            }
        }
    }
}

