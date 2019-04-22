using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject Weapons;
    public Transform cam;

    // <>
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit,7))
        {
            if (hit.transform.gameObject.GetComponent<GunPickupObject>() != null && Input.GetKeyDown("q"))
            {          
                    foreach (Slot s in Weapons.GetComponent<WeaponManager>().Slots)
                    {
                        if (!s.SlotFull)
                        {
                            hit.transform.gameObject.GetComponent<PhotonView>().RequestOwnership();
                            //   Weapons.GetComponent<WeaponManager>().Slots[s.SlotId].gameObject = Weapons.GetComponent<WeaponManager>().Weapons[other.GetComponent<GunPickupObject>().id];
                            Weapons.GetComponent<WeaponManager>().Weapons[hit.transform.GetComponent<GunPickupObject>().id].transform.parent = Weapons.GetComponent<WeaponManager>().Slots[s.SlotId].transform;
                            s.SlotFull = true;
                            Debug.Log("PICKING_UP_GUN");
                            PhotonNetwork.Instantiate(hit.transform.GetComponent<GunPickupObject>().nextWep, new Vector3(Random.Range(GameObject.Find("Left").transform.position.x, GameObject.Find("Right").transform.position.x), 100, Random.Range(GameObject.Find("Down").transform.position.x, GameObject.Find("Up").transform.position.x)), Quaternion.identity, 0);
                            PhotonNetwork.Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
            }            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
