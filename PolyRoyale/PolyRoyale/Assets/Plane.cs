using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float PlaneSpeed;
    public Transform Prop1;
    public Transform Prop2;
    public Transform Prop3;
    public Transform Prop4;

    // <>

    void Start()
    {
        
    }
  
    void Update()
    {       if (PhotonNetwork.otherPlayers.Length > 0)
            {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlaneSpeed);
            Prop1.Rotate(new Vector3(0, 0, 100));
            Prop2.Rotate(new Vector3(0, 0, 100));
            Prop3.Rotate(new Vector3(0, 0, 100));
            Prop4.Rotate(new Vector3(0, 0, 100));

           }
    }
}
