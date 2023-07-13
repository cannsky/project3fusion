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
            if (tower.towerData != null && tower.towerData.Value != null && tower.towerData.Value.isSet)
            {
                Setup();
                break;
            }
            else yield return null;
        }
        yield return null;
    }

    public void Setup()
    {
        if (tower.IsClient) ClientSetup();
        if (tower.IsServer) ServerSetup();
        tower.isReady = true;
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
