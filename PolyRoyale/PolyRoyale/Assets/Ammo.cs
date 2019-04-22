using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{

    // <>

    public int Small;
    public int Middle;
    public int Heavy;

    public Text SmallTxt;
    public Text MiddleTxt;
    public Text HeavyTxt;

    private void Start()
    {
        SmallTxt = GameObject.Find("Canvas").transform.Find("Ammo").transform.Find("Small").GetComponent<Text>();
        MiddleTxt = GameObject.Find("Canvas").transform.Find("Ammo").transform.Find("Middle").GetComponent<Text>();
        HeavyTxt = GameObject.Find("Canvas").transform.Find("Ammo").transform.Find("Heavy").GetComponent<Text>();
        Small = 100;
        Middle = 100;
        Heavy = 100;
    }
    void Update()
    {
        SmallTxt.text =  "Small Ammo : " + Small.ToString();
        MiddleTxt.text = "Middle Ammo : " + Middle.ToString();
        HeavyTxt.text = "Heavy Ammo : " + Heavy.ToString();
    }
}
