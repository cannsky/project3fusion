using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerManagerTowerStarterCoroutine
{
    public static IEnumerator Coroutine()
    {
        while (true)
        {
            if (ServerManager.Instance.IsServer)
            {
                Setup();
                break;
            }
            else if (ServerManager.Instance.IsClient) break;
            else yield return null;
        }
    }

    public static void Setup()
    {
        foreach (GameObject towerGameObject in ServerManager.Instance.towers)
        {
            towerGameObject.GetComponent<NetworkObject>().enabled = true;
            towerGameObject.GetComponent<Tower>().enabled = true;
        }
    }
}
