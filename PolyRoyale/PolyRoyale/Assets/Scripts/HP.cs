using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public GameObject KillFX;
    public float Health = 100;
    public Image Bar;
    public Text BarTxt;
    // <>
    void Start()
    {
        Bar = GameObject.Find("Canvas").transform.Find("Health").transform.Find("Bar").GetComponent<Image>();
        BarTxt = GameObject.Find("Canvas").transform.Find("Health").transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            Respawn();

        if (GetComponent<PhotonView>().isMine)
        {
            Bar.fillAmount = Health / 100;            
            BarTxt.text = Health.ToString();
        }
    }
    void Respawn()
    {
        PhotonNetwork.Instantiate("KillFX", transform.position, Quaternion.identity,0);
        transform.position = GameObject.Find("Respawnpoint").transform.position;
        if (GetComponent<PhotonView>().isMine)
         GetComponent<CharacterController>().enabled = false;

        Health = 100;

        if (GetComponent<PhotonView>().isMine)
            GetComponent<CharacterController>().enabled = true;
    }    
}
