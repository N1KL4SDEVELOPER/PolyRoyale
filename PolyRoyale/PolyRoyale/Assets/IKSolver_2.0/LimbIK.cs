using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LimbIK : MonoBehaviour {


    public Vector3 elbowForward = Vector3.back;
    public Transform upperLimb;
    public Transform lowerLimb;
    public Transform endLimb;
    public Transform target;
    public WeaponManager Weapons;
    public Transform PistolOrigPos;
    public Transform PistolOrigOrigPos;
    public Transform PCOrigPos;
    public Transform ScarOrigPos;
    public Transform PCOrigOrigPos;
    public Transform ScarOrigOrigPos;
    public Transform Pos;
    public bool LeftHand;
  

    public Transform OrigHandRot;
    public Transform NormalHandRot;
    public Transform UnarmedHandRot;


    public float Amount;
    //<>[]
    private void Start()
    {
       
    }
    void LateUpdate ()
    {
        if(GetComponentInParent<VehicleGetIn>().inVeh && !LeftHand)
        {
            IKSolver.Solve(false, 1f, 1f, 0f, -1, -1, upperLimb, lowerLimb, endLimb, elbowForward, target.position, Vector3.zero, target.rotation);
            endLimb.rotation = Quaternion.identity;
            if (Amount > 0)
                target.position = GetComponentInParent<VehicleGetIn>().lastVeh.transform.Find("WeaponPos").position;
            else
                target.position = GetComponentInParent<VehicleGetIn>().lastVeh.transform.Find("Model").transform.Find("SW").transform.Find("R").position;
        }


        if (GetComponent<PhotonView>().isMine)
        {
            if (!LeftHand)
            {
                if (Amount > 0)
                {
                    Amount -= Time.deltaTime;
                    endLimb.transform.Find("Hand_R").transform.rotation = NormalHandRot.rotation;
                    endLimb.transform.Find("Hand_R").transform.position = NormalHandRot.position;
                }
                else
                {
                    endLimb.transform.Find("Hand_R").transform.rotation = NormalHandRot.rotation;
                    endLimb.transform.Find("Hand_R").transform.position = NormalHandRot.position;
                    Weapons.transform.rotation = Weapons.transform.parent.rotation;
                }


                if (Weapons.Slots[Weapons.currentSlot].transform.childCount != 0 && !GetComponent<AnimationHandler>().run && !GetComponent<AnimationHandler>().runR && !GetComponent<AnimationHandler>().runL)
                {
                    if (Amount > 0)
                        IKSolver.Solve(false, 1f, 1f, 0f, -1, -1, upperLimb, lowerLimb, endLimb, elbowForward, target.position, Vector3.zero, target.rotation);

                PistolOrigPos.position = PistolOrigOrigPos.position;
                PCOrigPos.position = PCOrigOrigPos.position;
                ScarOrigPos.position = ScarOrigOrigPos.position;
                }
            if (GetComponent<AnimationHandler>().run  && Weapons.Slots[Weapons.currentSlot].transform.childCount != 0 || GetComponent<AnimationHandler>().runR && Weapons.Slots[Weapons.currentSlot].transform.childCount != 0 || GetComponent<AnimationHandler>().runL && Weapons.Slots[Weapons.currentSlot].transform.childCount != 0)
                {
                    if (Amount > 0)
                        IKSolver.Solve(false, 1f, 1f, 0f, -1, -1, upperLimb, lowerLimb, endLimb, elbowForward, target.position, Vector3.zero, target.rotation);

                PistolOrigPos.position = Pos.position;
                PCOrigPos.position = Pos.position;
                ScarOrigPos.position = Pos.position;

            }
                
                
            }
            else
            {
                if (Weapons.BothHanded && Amount > 0 && !GetComponent<AnimationHandler>().strafeR && !GetComponent<AnimationHandler>().strafeL && !GetComponent<AnimationHandler>().runR && !GetComponent<AnimationHandler>().runL)
                {                  
                        Amount -= Time.deltaTime;
                        IKSolver.Solve(false, 1f, 1f, 0f, -1, -1, upperLimb, lowerLimb, endLimb, elbowForward, target.position, Vector3.zero, target.rotation);
                   
                }                
            }
        }
        else
        {
            if (Amount > 0 && !LeftHand)
            {
                Amount -= Time.deltaTime;
                
                endLimb.transform.Find("Hand_R").transform.rotation = OrigHandRot.rotation;
                endLimb.transform.Find("Hand_R").transform.position = OrigHandRot.position;
            }
            else
            {
                /*
                if (Weapons.Slots[Weapons.SyncCurrentSlot].transform.childCount != 0)
                {
                    endLimb.transform.Find("Hand_R").transform.rotation = NormalHandRot.rotation;
                    endLimb.transform.Find("Hand_R").transform.position = NormalHandRot.position;
                }
                else
                {
                    endLimb.transform.Find("Hand_R").transform.rotation = UnarmedHandRot.rotation;
                    endLimb.transform.Find("Hand_R").transform.position = UnarmedHandRot.position;
                }
                */
                
                Weapons.transform.rotation = Weapons.transform.parent.rotation;
            }



            if (!LeftHand)
            {
               

                if (Weapons.Slots[Weapons.currentSlot].transform.childCount != 0 && GetComponent<Animator>().GetBool("Run") == false)
                {
                    if (Amount > 0 || Weapons.isAiming)
                        IKSolver.Solve(false, 1f, 1f, 0f, -1, -1, upperLimb, lowerLimb, endLimb, elbowForward, target.position, Vector3.zero, target.rotation);


                    PistolOrigPos.position = PistolOrigOrigPos.position;
                    PCOrigPos.position = PCOrigOrigPos.position;
                    ScarOrigPos.position = ScarOrigOrigPos.position;
                }
            if (GetComponent<Animator>().GetBool("Run") == true && Weapons.Slots[Weapons.currentSlot].transform.childCount != 0)
            {
                    if (Amount > 0 || Weapons.isAiming)
                        IKSolver.Solve(false, 1f, 1f, 0f, -1, -1, upperLimb, lowerLimb, endLimb, elbowForward, target.position, Vector3.zero, target.rotation);

                    PistolOrigPos.position = Pos.position;
                    PCOrigPos.position = Pos.position;
                    ScarOrigPos.position = Pos.position;

                }
            }
             else
             {
                if (Weapons.BothHanded && !GetComponent<Animator>().GetBool("StrafeR") && !GetComponent<Animator>().GetBool("StrafeL") && !GetComponent<Animator>().GetBool("RunR") && !GetComponent<Animator>().GetBool("RunL"))
                {
                    

                    if (Amount > 0 || Weapons.isAiming)
                        IKSolver.Solve(false, 1f, 1f, 0f, -1, -1, upperLimb, lowerLimb, endLimb, elbowForward, target.position, Vector3.zero, target.rotation);

                }
             }
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (stream.isWriting)
            {
                stream.SendNext(Amount);
            }
        }
        else
        {
            if (stream.isReading)
            {
             Amount = (float)stream.ReceiveNext();
            }
        }
    }
}
