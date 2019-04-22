using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    // <>
    void Start()
    {
        
    }

    
    void Update()
    {
        PlayerPrefs.SetString("NAME", transform.Find("Text").GetComponent<Text>().text);
    }
}
