using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool SlotFull;
    public int SlotId;
    public int WepInSlot;

    // []

    private void Update()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (transform.childCount != 0)
                WepInSlot = GetComponentInChildren<Gun>().id;
        }
        else
        {
            transform.parent.GetComponent<WeaponManager>().Weapons[WepInSlot].transform.parent = transform;
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (stream.isWriting)
            {
                stream.SendNext(WepInSlot);             
            }
        }
        else
        {
            if (stream.isReading)
            {
               WepInSlot = (int)stream.ReceiveNext();             
            }
        }
    }

}
