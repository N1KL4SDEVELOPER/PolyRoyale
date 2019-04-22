using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
public class Stats : MonoBehaviour
{
    public int Kills;
    public Text KillsTxt;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    // <>
    void Start()
    {
        KillsTxt = transform.Find("Kills").GetComponent<Text>();
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

     
    void Update()
    {
        KillsTxt.text = Kills.ToString();
        if (Kills == 10)
        {
            WinScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            
        }
        foreach (Check c in FindObjectsOfType<Check>())
        {
            if (!c.transform.GetComponent<PhotonView>().isMine && c.WinSync == true)
                LoseScreen.SetActive(true);
                
        }
        }
    public void BackToLobby()
    {
        SceneManager.LoadScene(0);
        foreach(FirstPersonController f in FindObjectsOfType<FirstPersonController>())
        {
            if (f.transform.GetComponent<PhotonView>().isMine)
                PhotonNetwork.Destroy(f.gameObject);
        }
    }
}
