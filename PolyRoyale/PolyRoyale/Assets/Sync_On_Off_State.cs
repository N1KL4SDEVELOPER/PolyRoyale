using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <>

[ExecuteInEditMode]
public class Sync_On_Off_State : MonoBehaviour
{
    public GameObject From;
    public GameObject FromParent;
    public bool Test;
    public List<GameObject> To = new List<GameObject>();

    void Update()
    {
        if(From.GetComponentInParent<Slot>() == null)
        {
            foreach (GameObject to in To)
            to.SetActive(From.activeSelf);
        }
        if (From.GetComponentInParent<Slot>() != null)
        {
            FromParent = From.transform.parent.gameObject;
            /*
            foreach (GameObject to in To)
            {
                if (FromParent.activeSelf == true)
                    to.SetActive(true);
                else
                    to.SetActive(false);
            }
            */

            Test = FromParent.active;
            
            Debug.Log(FromParent.activeSelf);
        }
    }
}
