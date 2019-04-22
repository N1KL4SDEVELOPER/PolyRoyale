using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Characters.FirstPerson;
public class VehicleGetIn : MonoBehaviour
{
    public GameObject cam;
    bool lookingAtVeh;
    public bool inVeh;
    public GameObject lastVeh;
    public Transform SW;
    public Transform SWTargetRotation;
    public Transform CamTarget;
    public Transform OrigCamTarget;
    public PlayerInPlane PiP;
    public List<Collider> Colls = new List<Collider>();

    public float SteeringWheelRot;

    void Start()
    {
        if (cam == null)
            cam = transform.Find("FirstPersonCharacter").gameObject;

        SWTargetRotation = new GameObject().transform;
        SWTargetRotation.name = "SW_TAR_ROT";
    }

    // <>
    void Update()
    {

        if (GetComponent<PhotonView>().isMine)
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                if (hit.transform.tag == "Vehicle" && !inVeh)
                {
                    lookingAtVeh = true;
                    lastVeh = hit.transform.gameObject;
                }
                else
                {
                    lookingAtVeh = false;
                }

            }
            if (inVeh)
            {
                SW = lastVeh.transform.Find("Model").transform.Find("SW");
                //SW = GameObject.Find("SW").transform;

                if (Input.GetKey("d") && SWTargetRotation.rotation.z > -45)
                    SWTargetRotation.Rotate(0, 0, -5);

                if (Input.GetKey("a") && SWTargetRotation.rotation.z < 45)
                    SWTargetRotation.Rotate(0, 0, 5);

                if (!Input.GetKey("a") && !Input.GetKey("d"))
                    SWTargetRotation.rotation= new Quaternion(SWTargetRotation.rotation.x,SWTargetRotation.rotation.y,0,SWTargetRotation.rotation.w);

               
                //SW.localRotation = SWTargetRotation;
                SW.rotation = Quaternion.Slerp(SW.transform.rotation, new Quaternion(SW.transform.parent.rotation.x,SW.transform.parent.rotation.y,SWTargetRotation.rotation.z,SW.transform.parent.rotation.w), Time.deltaTime * 4);


                foreach (Collider c in Colls)
                    c.enabled = false;

                GetComponent<CharacterController>().enabled = false;
                GetComponent<FirstPersonController>().enabled = false;
                transform.parent = lastVeh.transform;               
                transform.position = lastVeh.transform.Find("Seat").position;
                transform.rotation = lastVeh.transform.Find("Seat").rotation;
                CamTarget.position = lastVeh.transform.Find("CamTarget").position;
                CamTarget.rotation = lastVeh.transform.Find("CamTarget").rotation;
                if (Input.GetKeyDown("e"))
                {
                    inVeh = false;
                    lastVeh.GetComponent<CarUserControl>().enabled = false;
                    CamTarget.position = OrigCamTarget.position;
                }
            }
            else
            {
                foreach (Collider c in Colls)
                    c.enabled = true;

                transform.parent = null;
                if(PiP.enabled == false)
                GetComponent<CharacterController>().enabled = true;

                GetComponent<FirstPersonController>().enabled = true;
            }
            if (lookingAtVeh)
            {
                if (Input.GetKeyDown("e"))
                {
                    lastVeh.GetPhotonView().RequestOwnership();
                    lastVeh.GetComponent<CarUserControl>().enabled = true;
                    inVeh = true;
                }
            }
        }
        else
        {/////////////////////////////////////////////////////////////////
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                if (hit.transform.tag == "Vehicle")
                {
                    lookingAtVeh = true;
                    lastVeh = hit.transform.gameObject;
                }
                else
                {
                    lookingAtVeh = false;
                }

            }
            if (inVeh)
            {
                foreach (Collider c in Colls)
                    c.enabled = false;

                transform.parent = lastVeh.transform;
                transform.position = lastVeh.transform.Find("Seat").position;
                transform.rotation = lastVeh.transform.Find("Seat").rotation;
            }
            else
            {
                foreach (Collider c in Colls)
                    c.enabled = true;

                transform.parent = null;              
            }           
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (stream.isWriting)
            {
                stream.SendNext(inVeh);
                
            }
        }
        else
        {
            if (stream.isReading)
            {
                inVeh = (bool)stream.ReceiveNext(); 
                
            }
        }
    }
}
