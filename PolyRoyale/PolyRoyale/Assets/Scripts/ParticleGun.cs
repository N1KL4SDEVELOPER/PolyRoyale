using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGun : MonoBehaviour
{

    public LimbIK IK;
    public Transform OrigTarPos;
    public Transform AimTarPos;
    public GameObject HandTarget;
    public string Muzzle;
    public string Bullet;
    public Transform BS;
    public Transform OrigPos;
    public float StartTimeBtwShots;
    float TimeBtwShots;
    public Transform cam;
    public Transform CamTarget;
    public GameObject Player;
    public bool Aim;
    public bool AutoFire;
    GameObject BulletOj;
    GameObject MuzzleOj;
    public bool BothHanded;


    void Start()
    {
        BS = transform.Find("BS").transform;

    }

    // <>
    void Update()
    {
        HandTarget.transform.position = OrigPos.transform.position;
        if (MuzzleOj != null)
            MuzzleOj.transform.position = BS.position;

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
                    IK.Amount = 0.1f;
                }
                else
                {
                    CamTarget.position = OrigTarPos.position;
                }

                RaycastHit hit;

                if (Physics.Raycast(cam.position, cam.forward, out hit))
                {
                    if (hit.transform.gameObject != Player && IK.Amount > 0)
                    {
                        transform.LookAt(hit.point);
                        //Player.transform.Find("CamPoint").transform.position = hit.point;
                    }
                    else
                    {
                        transform.rotation = transform.parent.rotation;
                    }
                }
                if (Input.GetMouseButtonDown(0) && TimeBtwShots <= 0)
                {
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
                    IK.Amount = 0.1f;
                }
                else
                {
                    CamTarget.position = OrigTarPos.position;
                }
                RaycastHit hit;

                if (Physics.Raycast(cam.position, cam.forward, out hit))
                {
                    if (hit.transform.gameObject != Player && IK.Amount > 0)
                    {
                        transform.LookAt(hit.point);
                        //Player.transform.Find("CamPoint").transform.position = hit.point;
                    }
                    else
                    {
                        transform.rotation = transform.parent.rotation;
                    }
                }
                if (Input.GetMouseButton(0) && TimeBtwShots <= 0)
                {
                    Invoke("Shoot", 0.02f);
                    TimeBtwShots = StartTimeBtwShots;
                }
                else
                {
                    TimeBtwShots -= Time.deltaTime;
                }
            }
        }
    }

    void Shoot()
    {
        IK.Amount = 1;
        BulletOj = PhotonNetwork.Instantiate(Muzzle, BS.transform.position, transform.rotation, 0);
        BulletOj = PhotonNetwork.Instantiate(Bullet, BS.transform.position, transform.rotation, 0);
        BulletOj.GetComponent<ParticleDamage>().Creator = Player;
    }
}
