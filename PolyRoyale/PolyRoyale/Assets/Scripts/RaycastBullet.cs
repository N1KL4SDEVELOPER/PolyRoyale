using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBullet : MonoBehaviour
{
    public GameObject Creator;
    public GameObject myPlayer;
    public float range;
    public GameObject impactEffect;
    public float Damage = 25;
    public float PushForce;
    public GameObject FX;
    public Transform cam;
    // <>
    void Start()
    {
      
        myPlayer = GameObject.Find("Manager").GetComponent<Manager>().Player;
      

        RaycastHit hit;
        
        if(Physics.Raycast(transform.position,transform.forward,out hit, range))
        {
           
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            GameObject FXoj = Instantiate(FX, transform.position, transform.rotation);
            FXoj.transform.localScale = new Vector3(FXoj.transform.localScale.x, FXoj.transform.localScale.y, Vector3.Distance(transform.position, hit.point));
            FXoj.transform.LookAt(hit.point);
            if (hit.transform.GetComponent<HP>() != null)
            {               
                if (Creator != null)
                {            
                    if (hit.transform.GetComponent<HP>().Health <= Damage)
                     GameObject.Find("Canvas").GetComponent<Stats>().Kills += 1;

                  
                }
                else
                {
                    GameObject DC = PhotonNetwork.Instantiate("DmgCounter", hit.point, Quaternion.identity, 0);
                    Debug.Log(myPlayer);
                    DC.GetComponentInChildren<DamageCounter>().Creator = myPlayer;
                    DC.GetComponentInChildren<DamageCounter>().Damage = Damage;              
                    hit.transform.GetComponent<HP>().Health -= Damage;

                    //if (hit.transform.GetComponent<HP>().Health <= Damage)
                    // PhotonNetwork.Instantiate(FX.name, hit.point, Quaternion.identity, 0);
                }
            }
            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * PushForce);
        }
    }
}
