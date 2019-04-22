using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Check : MonoBehaviour
{
  
    PhotonStream stream;
    public bool Win;
    public bool WinSync;
    public Camera CurWepCam;
    public Camera SniperScopeCam;
    // <>
    void Start()
    {
        if (!GetComponent<PhotonView>().isMine)
        {

            transform.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = false;
            transform.Find("FirstPersonCharacter").GetComponent<AudioListener>().enabled = false;
            transform.Find("FirstPersonCharacter").GetComponent<FlareLayer>().enabled = false;
            GetComponent<FirstPersonController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<PlayerInPlane>().enabled = false;
            CurWepCam.gameObject.SetActive(false);
            SniperScopeCam.gameObject.SetActive(false);
        }
        else
        {
            name = PlayerPrefs.GetString("NAME");
           
        }
    }


    void Update()
    {
        if (GetComponent<PhotonView>().isMine)
        {
           if(GameObject.Find("Canvas").GetComponent<Stats>().Kills == 10)
            {
                GetComponent<FirstPersonController>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
                Win = true;
            }
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (stream.isWriting)
            {
                stream.SendNext(name);
                stream.SendNext(Win);
            }
        }
        else
        {
            if (stream.isReading)
            {
                name = stream.ReceiveNext().ToString();
                WinSync = (bool)stream.ReceiveNext();
            }
        }
    }
}
