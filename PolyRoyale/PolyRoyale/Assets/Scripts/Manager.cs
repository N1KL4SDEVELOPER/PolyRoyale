using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // <>

   
    public Transform PlaneSpawnPoint;
    public GameObject PlaneClone;
    public GameObject Plane;
    public bool connected;
    public GameObject Player;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("v1");
        foreach (Car c in GameObject.FindObjectsOfType<Car>())
        {
            Debug.Log("Car");
            if (c.gameObject.GetComponent<PhotonView>() != null && c.gameObject.GetComponent<Check>() == null)
            {
                PhotonTransformView view = c.gameObject.GetComponent<PhotonTransformView>();
                view.m_PositionModel.SynchronizeEnabled = true;
                view.m_RotationModel.SynchronizeEnabled = true;

                PhotonView pView = c.gameObject.GetComponent<PhotonView>();
                
                pView.ObservedComponents = new List<Component>();
              
                pView.ObservedComponents.Add(view);
            }
        }
    }

    private void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED TO MASTER");
        PhotonNetwork.JoinLobby();
    }
    void OnJoinedLobby()
    {
        Debug.Log("CONNECTED TO LOBBY");
        PhotonNetwork.JoinRandomRoom();
    }
    void OnPhotonRandomJoinFailed()
    {       
        Debug.Log("RANDOM JOIN FAILED");
        PhotonNetwork.CreateRoom(null);
    }
    void OnJoinedRoom()
    {
            connected = true;
            PlaneClone = Instantiate(Plane, PlaneSpawnPoint.position, PlaneSpawnPoint.rotation);
            Player = PhotonNetwork.Instantiate("myPlayer", PlaneClone.transform.Find("Plane").transform.Find("SpawnPoint").position, Quaternion.identity, 0);
            Player.GetComponent<PlayerInPlane>().Plane = PlaneClone;      
    }
    
    void Update()
    {
        
    }
}
