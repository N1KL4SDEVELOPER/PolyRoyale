using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//<>
public class Bullet : MonoBehaviour
{
    public float Force;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Force);
    }

   
    void Update()
    {
        
    }
}
