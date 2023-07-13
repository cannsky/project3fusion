using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAwaitSetupCoroutine
{
    private Tower tower;

    public TowerAwaitSetupCoroutine(Tower tower) => this.tower = tower;

    public IEnumerator Coroutine()
    {
        while (true)
        {
            if (tower.towerData != null && tower.towerData.Value.isSet)
            {
                Setup();
                break;
            }
            else yield return null;
        }
    }

    public void Setup()
    {
        tower.isReady = true;
        if (tower.IsClient) ClientSetup();
        if (tower.IsServer) ServerSetup();
    }

    public void ClientSetup()
    {
        tower.towerUI = new TowerUI(tower);
    }

    public void ServerSetup()
    {
        tower.towerAttack = new TowerAttack(tower);
    }
}
