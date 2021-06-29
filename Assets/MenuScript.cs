using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;
using MLAPI.Transports.UNET;
using System;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public InputField InputField;

    public void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, MLAPI.NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approved = false;

        // connection data is approved we join
        string password = System.Text.Encoding.ASCII.GetString(connectionData);

        if (password == "mygame")
        {
            approved = true;
        }

        callback(true, null, approved,new Vector3(0,4,0), Quaternion.identity);
    }

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
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("mygame");

        NetworkManager.Singleton.StartClient();
        MenuPanel.SetActive(false);
    }


}
