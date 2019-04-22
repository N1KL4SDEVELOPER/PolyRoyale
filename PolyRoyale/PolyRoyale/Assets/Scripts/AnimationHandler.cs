using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Vector3 origPos;
    void Start()
    {
        origPos = transform.position;
        OrigRot = transform.rotation;
       
    }

    public WeaponManager Weapons;
    public SyncSpine Spine;
    public int AvatarSync;

    public bool turning;
    public bool walk;
    public bool run;
    public bool idle;
    public bool walkBack;
    public bool strafeR;
    public bool strafeL;
    public bool rotateR;
    public bool rotateL;
    public bool runR;
    public bool runL;
    public bool FloatingDown;
         
    public Avatar Normal;
    public Avatar StrafeL;
    public Avatar StrafeR;
    public Avatar Falling;
    public Quaternion OrigRot;

    public VehicleGetIn VGI;

    public Transform RHT;
    public Transform LHT;




    // <>[] ||

    void Update()
    {
        transform.position = transform.parent.position - new Vector3(0, 0.8f , 0);
        if(!VGI.inVeh)
        {
            //unarmed_walk_backwards 1
            //Arm.position = new Vector3(Arm.position.x, ArmPoint.position.y, ArmPoint.position.z);
            if (GetComponent<PhotonView>().isMine)
        {
          
            if (GetComponentInParent<PlayerInPlane>().AlreadyOnGround == true)
            {
                GetComponent<Animator>().SetBool("StrafeR", strafeR);
                GetComponent<Animator>().SetBool("StrafeL", strafeL);
                GetComponent<Animator>().SetBool("RotateL", rotateL);
                GetComponent<Animator>().SetBool("RotateR", rotateR);
                GetComponent<Animator>().SetBool("Idle", idle);
                GetComponent<Animator>().SetBool("Walk", walk);
                GetComponent<Animator>().SetBool("WalkBack", walkBack);
                GetComponent<Animator>().SetBool("Run", run);
                GetComponent<Animator>().SetBool("RunR", runR);
                GetComponent<Animator>().SetBool("RunL", runL);
            }
          
            GetComponent<Animator>().SetBool("FloatingDown", FloatingDown);

           
            transform.rotation = Quaternion.Lerp(transform.rotation,OrigRot,10 * Time.deltaTime);
           
            transform.Find("Root").transform.position = transform.position;

            if (GetComponent<Animator>().avatar == Normal)
            {
                AvatarSync = 1;
            }
            if (GetComponent<Animator>().avatar == StrafeR || GetComponent<Animator>().avatar == StrafeL)
            {
                AvatarSync = 2;
            }
            if (GetComponent<Animator>().avatar == Falling)
            {
                AvatarSync = 3;
            }

            if (GetComponentInParent<PlayerInPlane>().AlreadyOnGround == false)
            {
                FloatingDown = true;
                GetComponent<Animator>().avatar = Falling;
            }
            else
            {
                FloatingDown = false;
               
            }
            if (Weapons.Slots[Weapons.currentSlot].transform.childCount != 0 && idle)
                {
                OrigRot = transform.parent.rotation;
                Spine.enabled = true;
                }
                else
                {
                Spine.enabled = false;
                }
            /*
            if (OrigRot.y < transform.parent.rotation.y - 1)
            {            
                GetComponent<Animator>().Play("right_turn");
                OrigRot.y += 10;
            }
            if (OrigRot.y > transform.parent.rotation.y + 1)
            {              
                GetComponent<Animator>().Play("left_turn");
                OrigRot.y -= 10;
            }
            */
            if (Input.GetKey("a") && Input.GetKey("w") && !runR && !runL && !FloatingDown)
            {
                rotateL = true;
                //transform.parent.rotation = new Quaternion(transform.parent.parent.rotation.x, transform.parent.parent.rotation.y - 45, transform.parent.parent.rotation.z, transform.parent.parent.rotation.w);
            }
            else
            {
                rotateL = false;
                //transform.parent.rotation = transform.parent.parent.rotation;
            }
            if (Input.GetKey("d") && Input.GetKey("w") && !runR && !runL && !FloatingDown)
            {
                rotateR = true;
               // transform.parent.rotation = new Quaternion(transform.parent.parent.rotation.x, transform.parent.parent.rotation.y + 45, transform.parent.parent.rotation.z, transform.parent.parent.rotation.w);
            }
            else
            {
                rotateR = false;
                //transform.parent.rotation = transform.parent.parent.rotation;
            }
            if (Input.GetKey("a") && !run && !walk && !walkBack && !runR && !runL && !FloatingDown)
            {
                if (!FloatingDown)
                    GetComponent<Animator>().avatar = StrafeL;
                //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z, transform.rotation.w);
                //GetComponent<Animator>().Play("strafe_f_l");
               
                strafeL = true;
               
            }
            else
            {
                strafeL = false;
            }
            if (Input.GetKey("d") && !run && !walk && !walkBack && !runR && !runL && !FloatingDown)
            {
                if (!FloatingDown)
                    GetComponent<Animator>().avatar = StrafeR;
                //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z, transform.rotation.w);

                //GetComponent<Animator>().Play("strafe_f_r");
               
                strafeR = true;
               
               
            }
            else
            {
                strafeR = false;
            }
            if (Input.GetKey("s"))
            {
                //GetComponent<Animator>().Play("unarmed_walk 0");
                
                walkBack = true;
                if(!FloatingDown)
                GetComponent<Animator>().avatar = Normal;

                OrigRot = transform.parent.rotation;
            }
            else
            {
                walkBack = false;
            }
            if (Input.GetAxis("Vertical") < 0.3f && !turning && !walkBack && !strafeL && !strafeR && !FloatingDown)
             {
                // GetComponent<Animator>().Play("Pistol Idle");
                idle = true;
                if (!FloatingDown)
                    GetComponent<Animator>().avatar = Normal;             
            }
            else
            {
                idle = false;
            }

            if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
            {
                //GetComponent<Animator>().Play("unarmed_walk");
                
                walk = true;
                if (!FloatingDown)
                    GetComponent<Animator>().avatar = Normal;
                OrigRot = transform.parent.rotation;
            }
            else
            {
                walk = false;
            }
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) && !runR && !runL)
            {
                //GetComponent<Animator>().Play("unarmed_run");
               
                run = true;
                if (!FloatingDown)
                    GetComponent<Animator>().avatar = Normal;
                OrigRot = transform.parent.rotation;
            }
            else
            {
                run = false;
            }
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) && Input.GetKey("d"))
            {
                //GetComponent<Animator>().Play("unarmed_run");
                runR = true;
                if (!FloatingDown)
                    GetComponent<Animator>().avatar = Normal;
                OrigRot = transform.parent.rotation;
            }
            else
            {
                runR = false;
            }
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) && Input.GetKey("a"))
            {
                //GetComponent<Animator>().Play("unarmed_run");
                runL = true;
                if (!FloatingDown)
                    GetComponent<Animator>().avatar = Normal;
                OrigRot = transform.parent.rotation;
            }
            else
            {
                runL = false;
            }

            /*
                        if (Input.GetAxis("Horizontal")  == 0)
                    {
                        turning = false;
                    }

                    if (Input.GetAxis("Horizontal") < 0 && !turning && Input.GetAxis("Vertical") == 0)
                    {
                        GetComponent<Animator>().Play("left_turn");
                        turning = true;
                    }
                    if (Input.GetAxis("Horizontal") > 0 && !turning && Input.GetAxis("Vertical") == 0)
                    {
                        GetComponent<Animator>().Play("right_turn");
                        turning = true;
                    }
                    */
            //Debug.Log(Input.GetAxis("Horizontal"));

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        else
        {
            if (AvatarSync == 1)
            {
                 GetComponent<Animator>().avatar = Normal;
            }
            if (AvatarSync == 2)
            {
                GetComponent<Animator>().avatar = StrafeR;
            }
            if (AvatarSync == 3)
            {
                GetComponent<Animator>().avatar = Falling;
            }
            /*
        
            GetComponent<Animator>().SetBool("Idle", idleSync);
            GetComponent<Animator>().SetBool("Walk", walkSync);
            GetComponent<Animator>().SetBool("WalkBack", walkBackSync);
            GetComponent<Animator>().SetBool("Run", runSync);
            GetComponent<Animator>().SetBool("StrafeR", strafeRSync);
            GetComponent<Animator>().SetBool("StrafeL", strafeLSync);
            GetComponent<Animator>().SetBool("RotateL", rotateLSync);
            GetComponent<Animator>().SetBool("RotateR", rotateRSync);
            GetComponent<Animator>().SetBool("RunR", runRSync);
            GetComponent<Animator>().SetBool("RunL", runLSync);

            if (idleSync == true)
            {
                //GetComponent<Animator>().Play("Pistol Idle");
                GetComponent<Animator>().avatar = Normal;
            }

            if (walkSync == true)
            {
                //GetComponent<Animator>().Play("unarmed_walk");
                GetComponent<Animator>().avatar = Normal;
            }

            if (runSync == true)
            {
                //GetComponent<Animator>().Play("unarmed_run");
                GetComponent<Animator>().avatar = Normal;
            }

            if (walkBackSync == true)
            {
                //GetComponent<Animator>().Play("unarmed_walk 0");
                GetComponent<Animator>().avatar = Normal;
            }

        
            if (strafeRSync == true)
            {
              //  GetComponent<Animator>().Play("strafe_f_r");
                GetComponent<Animator>().avatar = StrafeR;
            }
            
            if (strafeLSync == true)
            {
                // GetComponent<Animator>().Play("strafe_f_l");
                GetComponent<Animator>().avatar = StrafeL;
            }
            */
        }
        }
        else
        {            
            GetComponent<Animator>().avatar = StrafeL;
            GetComponent<Animator>().Play("Driving");
            //RHT.position = GetComponentInParent<VehicleGetIn>().lastVeh.transform.Find("Model").transform.Find("SW").transform.Find("R").position;
            //LHT.position = GetComponentInParent<VehicleGetIn>().lastVeh.transform.Find("Model").transform.Find("SW").transform.Find("L").position;  
            //RHT.position = VGI.lastVeh.transform.Find("Model").transform.Find("SW").transform.Find("R").position;
            LHT.position = VGI.lastVeh.transform.Find("Model").transform.Find("SW").transform.Find("L").position;
        }
    }
    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {     
        if (GetComponent<PhotonView>().isMine)
        {
            if (stream.isWriting)
            {
                stream.SendNext(AvatarSync);              
            }
        }
        else
        {
            if (stream.isReading)
            {
                AvatarSync = (int)stream.ReceiveNext();       
            }
        }        
    }
}
