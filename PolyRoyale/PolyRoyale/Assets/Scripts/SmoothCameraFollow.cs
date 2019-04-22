using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public float MoveSpeed;
    public float RotationSpeed;
    public Transform target;

    // <>

    void Update()
    {
         if(transform.position != target.position)
        transform.position = Vector3.Lerp(transform.position, target.position, MoveSpeed * Time.deltaTime);
       
        if (transform.rotation != target.rotation && GetComponentInParent<VehicleGetIn>().inVeh)
           transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, RotationSpeed * Time.deltaTime);
    }
}
