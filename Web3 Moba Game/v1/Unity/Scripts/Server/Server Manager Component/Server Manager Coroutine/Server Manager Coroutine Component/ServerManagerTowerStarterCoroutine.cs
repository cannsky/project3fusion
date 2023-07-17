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
            else if (ServerManager.Instance.IsClient && !ServerManager.Instance.IsHost)
            {
                Setup();
                break;
            }
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
    }
}
