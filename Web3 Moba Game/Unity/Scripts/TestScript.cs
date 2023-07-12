using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void Start()
    {
        /*
        NetworkManager.Singleton.StartServer();
        Debug.Log("server started!");
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            Debug.Log(id + " player entered!!!");
        };
        */
    }
    
    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
