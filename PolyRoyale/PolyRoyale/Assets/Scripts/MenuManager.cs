using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public SkinsManager Skins;
    public Text SkinNumber;
    public Text HatNumber;
    public Text ScarfNumber;

    // <>

    private void Update()
    {
        SkinNumber.text = (Skins.currentSkin + 1).ToString() + " / " + Skins.Skins.Count;
        HatNumber.text = (Skins.currentHat + 1).ToString() + " / " + Skins.Hats.Count;
        ScarfNumber.text = (Skins.currentScarf + 1).ToString() + " / " + Skins.Scarves.Count;
    }
    public void Play()
    {
        PlayerPrefs.SetInt("SKIN", Skins.currentSkin);
        PlayerPrefs.SetInt("HAT", Skins.currentHat);
        PlayerPrefs.SetInt("SCARF", Skins.currentScarf);
        SceneManager.LoadScene(1);      
    }
    public void NextSkin()
    {
        if (Skins.currentSkin < Skins.Skins.Count - 1)
            Skins.currentSkin++;
    }

    public void PreviousSkin()
    {
        if (Skins.currentSkin > 0)
            Skins.currentSkin--;
    }

    public void NextHat()
    {
        if (Skins.currentHat < Skins.Hats.Count - 1)
            Skins.currentHat++;
    }

    public void PreviousHat()
    {
        if (Skins.currentHat > 0)
            Skins.currentHat--;
    }
    public void NextScarf()
    {
        if (Skins.currentScarf < Skins.Scarves.Count - 1)
            Skins.currentScarf++;
    }
    public void PreviousScarf()
    {
        if (Skins.currentScarf > 0)
            Skins.currentScarf--;
    }
}
