using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuPanel;

    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        MenuPanel.SetActive(false);
    }

    public void JoinButton()
    {
        NetworkManager.Singleton.StartClient();
        MenuPanel.SetActive(false);
    }
}
