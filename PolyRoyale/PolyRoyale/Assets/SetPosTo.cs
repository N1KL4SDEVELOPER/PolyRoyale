using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosTo : MonoBehaviour
{
    public Transform Target;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Target.position.y, transform.position.z);
    }
}
