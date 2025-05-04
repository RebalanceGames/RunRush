using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamLekesi : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}