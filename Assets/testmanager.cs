using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testmanager : MonoBehaviour
{
    public Text text;
    public Image image;
    void Start()
    {
        text.text = "kontrol";
        image.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
