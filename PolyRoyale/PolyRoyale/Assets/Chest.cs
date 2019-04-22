using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    //<>[]

    public Transform ItemSpawn;

    public bool Open;
    public bool CompletlyOpen;
    public bool ItemsSpawned;

    public float OpenSpeed;

    public Transform Lid;
    public Transform LidTargetRot;

    public List<GameObject> Items = new List<GameObject>();
    public List<string> SpawnedItems = new List<string>();

    public int MinAmount;
    public int MaxAmount;

    int curRand;

    public Manager Manager;

    void Start()
    {
        if (Manager == null)
            Manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

   void Update()
    {
        if (Lid.rotation == LidTargetRot.rotation)
            CompletlyOpen = true;

        if (!ItemsSpawned && Manager.connected)
        {
           if(PhotonNetwork.otherPlayers.Length == 0)
           {
                for (int i = 0; i < Random.Range(MinAmount, MaxAmount); i++)
                {
                curRand = Random.Range(0, Items.Count);
                if (!SpawnedItems.Contains(Items[curRand].name + "(Clone)"))
                {
                    GameObject SpawnedGO = PhotonNetwork.Instantiate(Items[curRand].name, ItemSpawn.position, Quaternion.identity, 0);
                    SpawnedItems.Add(SpawnedGO.name);
                }
                else
                    i -= 1;
                }
           }

            ItemsSpawned = true;
        }
        if (Open)
        {
            Lid.rotation = Quaternion.RotateTowards(Lid.rotation, LidTargetRot.rotation, OpenSpeed);           
        }
        if(CompletlyOpen)
            enabled = false;
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (stream.isWriting)
            {
                stream.SendNext(Open);
            }
        }
        else
        {
            if (stream.isReading)
            {
               Open = (bool)stream.ReceiveNext();
            }
        }

    }
}
