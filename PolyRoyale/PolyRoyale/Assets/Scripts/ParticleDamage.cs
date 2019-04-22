using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    public GameObject Creator;     
    public float Damage = 25;
    public float DamageTxtVal = 50;
    public float Force;
    public GameObject Explosion;

    public GameObject DmgCounter;
    // <>
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * Force);
    }
    private void OnDestroy()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<HP>() != null)
        {
            if (Creator != null)
            {
                if (other.transform.GetComponent<HP>().Health <= Damage && !other.transform.GetComponent<PhotonView>().isMine)
                    GameObject.Find("Canvas").GetComponent<Stats>().Kills += 1;

                GameObject DC = Instantiate(DmgCounter, transform.position, Quaternion.identity);
                DC.GetComponent<DamageCounter>().Creator = Creator;
                DC.GetComponent<DamageCounter>().Damage = DamageTxtVal;
            }
            if(!other.transform.GetComponent<PhotonView>().isMine)
            other.transform.GetComponent<HP>().Health -= Damage;

        }
       
            if(other.transform.GetComponent<PhotonView>() == null)
            {
                Destroy(gameObject);
            }
            else
            {
            if (!other.transform.GetComponent<PhotonView>().isMine)
                Destroy(gameObject);
        }
    }
}
