using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject Player;
    public GameObject BP;
    float xPos1;
    float yPos1;
    float zPos1;
    float xPos2;
    float yPos2;
    float zPos2;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PhotonNetwork.Instantiate("Wall", BP.transform.position, BP.transform.rotation, 0);
        }
        xPos1 = transform.Find("SP").transform.position.x / 3;
        yPos1 = transform.Find("SP").transform.position.y / 3;
        zPos1 = transform.Find("SP").transform.position.z / 3;
        xPos2 = Mathf.Round(xPos1);
        yPos2 = Mathf.Round(yPos1);
        zPos2 = Mathf.Round(zPos1);
        BP.transform.position = new Vector3(xPos2 * 3, yPos2 * 3, zPos2 * 3);
        //<>
        //BP.transform.position = transform.Find("SP").transform.position;
        var vec = Player.transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        BP.transform.eulerAngles = vec;
    }
}
