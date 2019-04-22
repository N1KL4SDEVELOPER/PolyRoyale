using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectToPoint : MonoBehaviour
{
    public Transform Target;
  
    void Update()
    {
        transform.rotation = Target.rotation;
    }
}
