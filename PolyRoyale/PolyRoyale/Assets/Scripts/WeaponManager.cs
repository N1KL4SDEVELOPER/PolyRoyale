using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour { 

    public List<Slot> Slots = new List<Slot>();
    public int currentWeapon;
    public int currentWeaponPlus;
    public int currentSlot;
    public int SyncCurrentSlot;
   
    public int SyncCurrentWeaponVal;
    public List<GameObject> Weapons = new List<GameObject>();
    public bool BothHanded;
    public Transform LeftHandTarget;
    public GameObject WeaponLeftHandTarget;

    public bool isAiming;
    // <>
    void Start()
    {
        
    }

    
    void Update()
    {
       

        if (GetComponent<PhotonView>().isMine)
        {
            foreach (GameObject g in Weapons)
            {
                if (g.transform.parent.GetComponent<Slot>() == null)
                    g.SetActive(false);
                else
                    g.SetActive(true);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                currentSlot++;

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                currentSlot--;

            if (currentSlot < 0)
                currentSlot = 4;

            if (currentSlot > 4)
                currentSlot = 0;

            foreach (Slot s in Slots)
            {
                if (s != Slots[currentSlot])
                    s.transform.gameObject.SetActive(false);
                else
                    s.transform.gameObject.SetActive(true);
            }
            if(Slots[currentSlot].transform.childCount != 0 && Input.GetMouseButton(1))
            {
                isAiming = true;
            }
            else
            {
                isAiming = false;
            }
            if (BothHanded)
                LeftHandTarget.position = Slots[currentSlot].GetComponentInChildren<Gun>().transform.Find("LeftHandTarget").position;

            if (Slots[currentSlot].GetComponentInChildren<ParticleGun>() != null)
            {
                BothHanded = Slots[currentSlot].GetComponentInChildren<ParticleGun>().BothHanded;
            }
            else
            {


                if (Slots[currentSlot].GetComponentInChildren<Gun>() != null)
                {
                    BothHanded = Slots[currentSlot].GetComponentInChildren<Gun>().BothHanded;
                }
            }
            /*
            currentWeaponPlus = currentWeapon + 1;
            if (BothHanded)
                LeftHandTarget.position = Weapons[currentWeapon].transform.Find("LeftHandTarget").position;

            if (Weapons[currentWeapon].GetComponent<ParticleGun>() != null)
            {
                BothHanded = Weapons[currentWeapon].GetComponent<ParticleGun>().BothHanded;
            }
            else
            {


                if (Weapons[currentWeapon].GetComponent<Gun>() != null)
                {
                    BothHanded = Weapons[currentWeapon].GetComponent<Gun>().BothHanded;
                }
            }

            foreach (GameObject g in Weapons)
        {
            if (g != Weapons[currentWeapon])
                g.SetActive(false);
            else
                g.SetActive(true);
        }


            foreach (GameObject g in Weapons)
            {
                if (g != Weapons[currentWeapon])
                    g.SetActive(false);
                else
                    g.SetActive(true);
            */
        }
        else
        {
            foreach (GameObject g in Weapons)
            {
                if (g.transform.parent.GetComponent<Slot>() == null)
                    g.SetActive(false);
                else
                    g.SetActive(true);
            }
            foreach (Slot s in Slots)
            {
                if (s != Slots[SyncCurrentSlot])
                    s.transform.gameObject.SetActive(false);
                else
                    s.transform.gameObject.SetActive(true);
            }
            
            if (Slots[SyncCurrentSlot].GetComponentInChildren<ParticleGun>() != null)
            {
                Debug.Log("WORKS");
                BothHanded = Slots[SyncCurrentSlot].GetComponentInChildren<ParticleGun>().BothHanded;

                if (BothHanded)
                    LeftHandTarget.position = Slots[SyncCurrentSlot].GetComponentInChildren<ParticleGun>().transform.Find("LeftHandTarget").position;
            }
            else
            {
                if (Slots[SyncCurrentSlot].GetComponentInChildren<Gun>() != null)
                {
                    Debug.Log("WORKS");
                    BothHanded = Slots[SyncCurrentSlot].GetComponentInChildren<Gun>().BothHanded;

                    if (BothHanded)
                    {
                        WeaponLeftHandTarget = Weapons[Slots[SyncCurrentSlot].WepInSlot].transform.Find("LeftHandTarget").gameObject;//LeftHandTarget.position = Weapons[Slots[SyncCurrentSlot].WepInSlot].transform.Find("LeftHandTarget").position;
                        LeftHandTarget.position = WeaponLeftHandTarget.transform.position;
                    }
                }
            }
            /*
            foreach (GameObject g in Weapons)
            {
                if (g != Weapons[int.Parse(SyncCurrentWeapon)])
                    g.SetActive(false);
                else
                    g.SetActive(true);
            }
            */
            /*
        SyncCurrentWeaponVal = int.Parse(SyncCurrentWeapon) - 1;

        if (BothHanded)
            LeftHandTarget.position = Weapons[SyncCurrentWeaponVal].transform.Find("LeftHandTarget").position;

        if (Weapons[SyncCurrentWeaponVal].GetComponent<ParticleGun>() != null)
        {
            BothHanded = Weapons[SyncCurrentWeaponVal].GetComponent<ParticleGun>();
        }
        else
        {
            if (Weapons[SyncCurrentWeaponVal].GetComponent<Gun>() != null)
            {
                BothHanded = Weapons[SyncCurrentWeaponVal].GetComponent<Gun>().BothHanded;
            }
        }
        foreach (GameObject g in Weapons)
        {
            if (g != Weapons[SyncCurrentWeaponVal])
                g.SetActive(false);
            else
                g.SetActive(true);
        }
        */
        }
    }
    
    

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (stream.isWriting)
            {
                stream.SendNext(currentWeapon);
                stream.SendNext(currentSlot);
                stream.SendNext(isAiming);
                stream.SendNext(BothHanded);
            }
        }
        else
        {
            if (stream.isReading)
            {
                SyncCurrentWeaponVal = (int)stream.ReceiveNext();
                SyncCurrentSlot = (int)stream.ReceiveNext();
                isAiming = (bool)stream.ReceiveNext();
                BothHanded = (bool)stream.ReceiveNext();
            }
        }
    }
}
