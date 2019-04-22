using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInPlane : MonoBehaviour
{
    public float MoveSpeed;
    public float FallSpeed;

    public GameObject Plane;
    public GameObject CamTarget;
    public GameObject OrigCamTarget;
    public bool InPlane;
    public bool FloatingDown;
    public bool AlreadyOnGround;
    public Transform GroundCheckPos;
    public Rigidbody rb;
    Vector3 Pos;




    void Start()
    {
        InPlane = true;
        FloatingDown = true;
        AlreadyOnGround = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().mass = 0.01f; 
    }

   
    void Update()
    {

        transform.position = new Vector3(transform.position.x , Pos.y, transform.position.z);
        if (InPlane)
        {
            transform.position = Plane.transform.position;
            CamTarget.transform.position = Plane.transform.Find("CamTarget").position;
            if (Input.GetKeyDown(KeyCode.Space))
                InPlane = false;

            GetComponent<CharacterController>().enabled = false;
           
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().mass = 0.01f;
            Pos = transform.position;

        }
        else
        {
            CamTarget.transform.position = OrigCamTarget.transform.position;
            Pos = new Vector3(0, Pos.y - FallSpeed, 0);
            

            if (!AlreadyOnGround)
            {
                if (Input.GetKey("w"))
                    rb.AddForce(transform.forward * MoveSpeed,ForceMode.Force);

                if (Input.GetKey("s"))
                    rb.AddForce(-transform.forward * MoveSpeed, ForceMode.Force);

                if (Input.GetKey("a"))
                    rb.AddForce(-transform.right * MoveSpeed, ForceMode.Force);

                if (Input.GetKey("d"))
                    rb.AddForce(transform.right * MoveSpeed, ForceMode.Force);
            }

            RaycastHit hit;
            if (Physics.Raycast(GroundCheckPos.position, -GroundCheckPos.up, out hit, 10) && hit.transform != transform)
            {
                Debug.Log(hit.transform.name);
                AlreadyOnGround = true;
                FloatingDown = false;
                GetComponent<CharacterController>().enabled = true;
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().mass = 1;
                DeactivateScript();
            }      
        }
    }
    private void OnTriggerStay(Collider other)
    {
        /*
        if(!other.transform == transform)
        {
            AlreadyOnGround = true;
        FloatingDown = false;
        GetComponent<CharacterController>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().mass = 1;
        CamTarget.transform.position = OrigCamTarget.transform.position;
       
        }
        */
    }
    public void DeactivateScript()
    {
        this.enabled = false;
    }
  }
