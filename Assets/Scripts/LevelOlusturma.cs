using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelOlusturma : ScriptableObject
{
    [System.Serializable]
    public struct Level
    { 
        public GameObject[] prefabs;
    }

    public Level[] levels;


    public List<string> levelstest;


    public void Test()
    {
        Debug.Log("SElam");
    }

    public void InstanteObj()
    {
        
    }
}