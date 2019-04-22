using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public SmoothCameraFollow CamFollow;
    public Transform SniperLookPoint;
    public bool Sniper;
    public Vector3 Offset;
    public WeaponManager WM;
    public Transform PosForHeavyWeapons;

    public Transform OrigParent;

    public bool Small;
    public bool Middle;
    public bool Heavy;

    public int id;

    public List<LimbIK> Ik = new List<LimbIK>();
    public GameObject HandTarget;
    public string Bullet;
    public Transform BS;
    
    public Transform OrigPos;
    public Transform OrigTarPos;
    public Transform AimTarPos;
    public float StartTimeBtwShots;
    float TimeBtwShots;
    public Transform cam;
    public Transform CamTarget;
    public GameObject Player;
    public bool Aim;
    public bool AutoFire;
    GameObject BulletOj;
    public bool BothHanded;
    public Transform FX;

    void Start()
    {
        BS = transform.Find("BS").transform;     
    }

    //<>[]

    void Update()
    {
        
       
            if (FX != null)
            FX.position = Vector3.MoveTowards(FX.position, transform.position, 2 * Time.deltaTime);

        HandTarget.transform.position = OrigPos.transform.position;
        if (BulletOj != null)
            BulletOj.transform.position = BS.transform.position;

        if (GetComponent<PhotonView>().isMine)
        {
            if (!AutoFire)
            {
                if (Input.GetMouseButtonDown(1))
                    Aim = true;

                if (Input.GetMouseButtonUp(1))
                    Aim = false;

                if (Aim == true)
                {
                    CamTarget.position = AimTarPos.position;

                    foreach(LimbIK IK in Ik)
                    IK.Amount = 0.1f;
                    if (Sniper)
                        CamFollow.MoveSpeed = 100000000;

                    if (Heavy)
                    {
                        transform.parent.parent = OrigParent;
                        transform.parent.position = OrigParent.position;
                        transform.parent.rotation = OrigParent.rotation;
                    }
                }
                else
                {
                    CamTarget.position = OrigTarPos.position;

                    if (Sniper)
                        CamFollow.MoveSpeed = 10;

                    if (Heavy && Ik[0].Amount <= 0)
                    {
                        transform.parent.parent =   PosForHeavyWeapons;
                        transform.parent.position = PosForHeavyWeapons.position;
                        transform.parent.rotation = PosForHeavyWeapons.rotation;
                    }
                }
                if(!Sniper)
                {
                    RaycastHit hit;

                if (Physics.Raycast(cam.position, cam.forward, out hit))
                {
                    if (hit.transform.gameObject != Player && Ik[0].Amount > 0)
                    {
                        transform.LookAt(hit.point + Offset);
                        Player.transform.Find("AimPoint").transform.position = hit.point;
                    }
                    else
                    {
                        transform.rotation = transform.parent.rotation;
                    }
                }
                }
                else
                {
                    if(Ik[0].Amount > 0)
                    transform.LookAt(SniperLookPoint.position);
                    //RaycastHit hit;

                    //if (Physics.Raycast(cam.position, cam.forward, out hit))
                    //{
                    //transform.LookAt(new Vector3(SniperLookPoint.position.x + Offset.x, SniperLookPoint.position.y + Offset.y, hit.point.z));
                    // }

                }
                if (Input.GetMouseButtonDown(0) && TimeBtwShots <= 0)
                {
                    foreach (LimbIK IK in Ik)
                        IK.Amount = 1;

                    Invoke("Shoot", 0.02f);
                    TimeBtwShots = StartTimeBtwShots;
                }
                else
                {
                    TimeBtwShots -= Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                    Aim = true;

                if (Input.GetMouseButtonUp(1))
                    Aim = false;

                if (Aim == true)
                {
                    CamTarget.position = AimTarPos.position;

                    foreach (LimbIK IK in Ik)
                        IK.Amount = 0.1f;

                    if (Sniper)
                        CamFollow.MoveSpeed = 100000000;


                    if (Heavy)
                    {
                        transform.parent.parent = OrigParent;
                        transform.parent.position = OrigParent.position;
                        transform.parent.rotation = OrigParent.rotation;
                    }
                }
                else
                {
                    CamTarget.position = OrigTarPos.position;

                    if (Sniper)
                        CamFollow.MoveSpeed = 100;


                    if (Heavy && Ik[0].Amount <= 0)
                    {
                        transform.parent.parent   =   PosForHeavyWeapons;
                        transform.parent.position = PosForHeavyWeapons.position;
                        transform.parent.rotation = PosForHeavyWeapons.rotation;
                    }
                }


                if (!Sniper)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(cam.position, cam.forward, out hit))
                    {
                        if (hit.transform.gameObject != Player && Ik[0].Amount > 0)
                        {
                            transform.LookAt(hit.point + Offset);
                            Player.transform.Find("AimPoint").transform.position = hit.point;
                        }
                        else
                        {
                            transform.rotation = transform.parent.rotation;
                        }
                    }
                }
                else
                {
                    //if (Ik[0].Amount > 0)
                      //  transform.LookAt(SniperLookPoint);
                    
                    RaycastHit hit;

                    if (Physics.Raycast(cam.position, cam.forward, out hit) && Ik[0].Amount > 0)
                    {
                        transform.LookAt(new Vector3(SniperLookPoint.position.x, SniperLookPoint.position.y,hit.point.z));
                    }
                 
                }
                if (Input.GetMouseButton(0) && TimeBtwShots <= 0)
                {
                    foreach (LimbIK IK in Ik)
                        IK.Amount = 1;

                    Invoke("Shoot", 0.02f);
                   
                    TimeBtwShots = StartTimeBtwShots;
                }
                else
                {
                    TimeBtwShots -= Time.deltaTime;
                }
            }
        }
        else
        {////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           
                Aim = WM.isAiming;

                if (Aim == true)
                {
                    CamTarget.position = AimTarPos.position;

                    foreach (LimbIK IK in Ik)
                        IK.Amount = 0.1f;

                    if (Heavy)
                    {
                        transform.parent.parent = OrigParent;
                        transform.parent.position = OrigParent.position;
                        transform.parent.rotation = OrigParent.rotation;
                    }
                }
                else
                {
                    CamTarget.position = OrigTarPos.position;

                    if (Heavy && Ik[0].Amount <= 0)
                    {
                        transform.parent.parent = PosForHeavyWeapons;
                        transform.parent.position = PosForHeavyWeapons.position;
                        transform.parent.rotation = PosForHeavyWeapons.rotation;
                    }
                }


            if (!Sniper)
            {
                RaycastHit hit;

                if (Physics.Raycast(cam.position, cam.forward, out hit))
                {
                    if (hit.transform.gameObject != Player && Ik[0].Amount > 0)
                    {
                        transform.LookAt(hit.point + Offset);
                        Player.transform.Find("AimPoint").transform.position = hit.point;
                    }
                    else
                    {
                        transform.rotation = transform.parent.rotation;
                    }
                }
            }
            else
            {
                if (Ik[0].Amount > 0)
                {
                    transform.LookAt(SniperLookPoint);
                    transform.parent.parent = OrigParent;
                    transform.parent.position = OrigParent.position;
                    transform.parent.rotation = OrigParent.rotation;
                }

                /*
                RaycastHit hit;

                if (Physics.Raycast(cam.position, cam.forward, out hit))
                {
                    transform.LookAt(new Vector3(SniperLookPoint.position.x + Offset.x, SniperLookPoint.position.y + Offset.y,hit.point.z));
                }
               */
            }

        }
            
    }
      
    
    void Shoot()
    {
        if (Small && Player.GetComponent<Ammo>().Small > 0)
        {
            Player.GetComponent<Ammo>().Small--;
            BulletOj = PhotonNetwork.Instantiate(Bullet, BS.transform.position, transform.rotation, 0);
            BulletOj.GetComponent<RaycastBullet>().Creator = Player;
        }
        if (Middle && Player.GetComponent<Ammo>().Middle > 0)
        {
            Player.GetComponent<Ammo>().Middle--;
            BulletOj = PhotonNetwork.Instantiate(Bullet, BS.transform.position, transform.rotation, 0);
            BulletOj.GetComponent<RaycastBullet>().Creator = Player;
        }
        if (Heavy && Player.GetComponent<Ammo>().Heavy > 0)
        {           
            transform.parent.parent = OrigParent;
            transform.parent.position = OrigParent.position;
            transform.parent.rotation = OrigParent.rotation;
            Player.GetComponent<Ammo>().Heavy--;
            BulletOj = PhotonNetwork.Instantiate(Bullet, BS.transform.position, BS.transform.rotation, 0);
            BulletOj.GetComponent<RaycastBullet>().Creator = Player;           
                
        }
    }
}
