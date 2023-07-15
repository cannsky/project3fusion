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
        foreach (Tower tower in ServerManager.Instance.towers)
        {
            if (tower == null) break;
            tower.GetComponent<NetworkObject>().enabled = true;
            tower.GetComponent<Tower>().enabled = true;
        }
        ServerManager.Instance.blueSpawner.GetComponent<NetworkObject>().enabled = true;
        ServerManager.Instance.blueSpawner.GetComponent<Spawner>().enabled = true;
        //ServerManager.Instance.redSpawner.GetComponent<NetworkObject>().enabled = true;
        //ServerManager.Instance.redSpawner.GetComponent<Spawner>().enabled = true;
    }
}
