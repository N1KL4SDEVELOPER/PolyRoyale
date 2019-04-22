using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public List<GameObject> Skins = new List<GameObject>();
    public List<GameObject> Hats = new List<GameObject>();
    public List<GameObject> Scarves = new List<GameObject>();
    public int currentSkin;
    public int currentHat;
    public int currentScarf;

    // <>

    private void Start()
    {
        if(name == "Character")
        {
            if (GetComponent<PhotonView>().isMine)
            {
                foreach (GameObject scarf in Scarves)
                {
                    if (scarf == Scarves[PlayerPrefs.GetInt("SCARF")])
                        scarf.SetActive(true);
                    else
                        scarf.SetActive(false);
                }

               foreach (GameObject skin in Skins)
               {
                  if (skin == Skins[PlayerPrefs.GetInt("SKIN")])
                  {
                      skin.SetActive(true);
                  }
                  else
                  {
                      skin.SetActive(false);
                  }
               }

            foreach (GameObject hat in Hats)
              {
                if (hat == Hats[PlayerPrefs.GetInt("HAT")])
                    hat.SetActive(true);
                else
                    hat.SetActive(false);
              }
            }
            else
            {
            Debug.Log(currentSkin);
            Debug.Log(currentHat);
             foreach (GameObject skin in Skins)
             {
                if (skin == Skins[currentSkin])
                    skin.SetActive(true);
                else
                    skin.SetActive(false);
             }

            foreach (GameObject hat in Hats)
            {
                if (hat == Hats[currentHat])
                    hat.SetActive(true);
                else
                    hat.SetActive(false);
            }
        }

      }
    }
    void Update()
    {
        if(name == "Character")
        {

        }
      else
      {
            foreach (GameObject skin in Skins)
            {
            if (skin == Skins[currentSkin])
                skin.SetActive(true);
            else
                skin.SetActive(false);
            }
            foreach (GameObject scarf in Scarves)
            {
                if (scarf == Scarves[currentScarf])
                    scarf.SetActive(true);
                else
                    scarf.SetActive(false);
            }

            foreach (GameObject hat in Hats)
            {
             if (hat == Hats[currentHat])
                 hat.SetActive(true);
             else
                 hat.SetActive(false);
            }
      }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            stream.SendNext(PlayerPrefs.GetInt("SKIN"));
            stream.SendNext(PlayerPrefs.GetInt("HAT"));
            stream.SendNext(PlayerPrefs.GetInt("SCARF"));
        }
        else
        {
            currentSkin = (int)stream.ReceiveNext();
            currentHat = (int)stream.ReceiveNext();
            currentScarf = (int)stream.ReceiveNext();
        }
    }
}
