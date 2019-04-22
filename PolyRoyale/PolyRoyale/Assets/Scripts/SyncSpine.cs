using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SyncSpine : MonoBehaviour
{

    public Transform Target;
    public Vector3 Offset; 

    Animator anim;
    public Transform chest;
    void Start()
    {
        anim = GetComponent<Animator>();
       

    }

    void LateUpdate()
    {
      
       // chest.LookAt(new Vector3(Target.position.x,chest.transform.position.y,Target.position.z));
        //chest.rotation = chest.rotation * Quaternion.Euler(Offset);      
    }
}