using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCounter : MonoBehaviour
{
    public float Damage;
    public GameObject Creator;
    public float MinForce;
    public float MaxForce;
    // <>
    void Start()
    {
        if (GetComponent<PhotonView>().isMine)
            GetComponent<TextMesh>().text = " ";
       

        GetComponent<Rigidbody>().AddForce(Random.Range(MinForce, MaxForce), 100, Random.Range(MinForce, MaxForce));
    }
    private void Update()
    {
        if (Creator != null)
            transform.LookAt(-Creator.transform.position);

        GetComponent<TextMesh>().text = Damage.ToString();
    }
      void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
      {
         if (GetComponent<PhotonView>().isMine)
         {
            if (stream.isWriting)
            {
                stream.SendNext(Damage);
            }
         }
         else
         {
            if (stream.isReading)
            {
                Damage = (float)stream.ReceiveNext();
            }
         }

      }
  }
