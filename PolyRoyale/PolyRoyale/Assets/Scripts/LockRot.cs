using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRot : MonoBehaviour
{
    //33,91
    void Update()
    {
        transform.rotation =   new Quaternion(33.91f, transform.rotation.y + transform.parent.rotation.y, transform.rotation.z + transform.parent.rotation.z, transform.rotation.w);
    }
}
