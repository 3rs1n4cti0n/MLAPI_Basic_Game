using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;
using MLAPI.Transports.UNET;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public InputField InputField;

    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        MenuPanel.SetActive(false);
    }

    public void JoinButton()
    {
        if (InputField.text.Length <= 0)
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
        }
        else
        {
            // clicked join
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = InputField.text;
        }
        NetworkManager.Singleton.StartClient();
        MenuPanel.SetActive(false);
    }
}
