using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TowerEvent
{
    private Tower tower;

    public TowerEvent(Tower tower) => this.tower = tower;

    public float ApplyDamage(float adDamage, float apDamage)
    {
        if (tower.towerData.Value.towerHealthData.ReduceHealth(adDamage + apDamage) <= 0) Die();
        return adDamage + apDamage;
    }

    public void Die()
    {
        tower.GetComponent<NetworkObject>().enabled = false;
        tower.enabled = false;
    }
}
