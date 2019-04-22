using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncObjects : MonoBehaviour
{
    // <>
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (other.GetComponent<PhotonView>() != null && other.GetComponent<Check>() == null)
                other.GetComponent<PhotonView>().RequestOwnership();
        }
        
    }
}
