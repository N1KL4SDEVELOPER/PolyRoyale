using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float delay;

    void Start()
    {
        if (GetComponent<PhotonView>().isMine)
        Invoke("DestroyOj", delay);
    }

    void DestroyOj()
    {
         PhotonNetwork.Destroy(gameObject);
    }
}
