using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public Transform cam;

    void Start()
    {
        
    }

   
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit,7))
        {
            if (hit.transform.gameObject.GetComponent<Chest>() != null && Input.GetKeyDown("e"))
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RequestOwnership();
                hit.transform.gameObject.GetComponent<Chest>().Open = true;
            }
        }
    }
}
