using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour
{
    public List<GameObject> counted = new List<GameObject>();
    // <>
    void Start()
    {
        //InvokeRepeating("Sync",1, 1);
    }

    private void LateUpdate()
    {
       
            foreach (Check c in FindObjectsOfType<Check>())
            {
            // for (int i = 0; i < counted.Count; i++)
            //{               
            //   if (c == counted[i])
            // {

            //}
            //else
            //{
            if (!counted.Contains(c.gameObject) && c.transform.name != "myPlayer(Clone)")
            {
                counted.Add(c.gameObject);
                GetComponent<Text>().text += c.name;
                GetComponent<Text>().text += ", ";
            }
                
                // }
                // }

                // }
            }
        
    }
    void Sync()
    {

    }
}

