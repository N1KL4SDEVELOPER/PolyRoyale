using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperControls : MonoBehaviour
{
    public string ObjectToSpawn;
    public Transform SpawnPoint;

    void Start()
    {
        
    }

   
    void Update()
    {
        if(ObjectToSpawn != "")
        {
            PhotonNetwork.Instantiate(ObjectToSpawn, SpawnPoint.transform.position, SpawnPoint.transform.rotation, 0);
            ObjectToSpawn = "";
        }
    }
}
