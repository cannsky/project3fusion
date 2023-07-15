using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MinionEvent
{
    private Minion minion;

    public MinionEvent(Minion minion) => this.minion = minion;

    public void ApplyDamage(float adDamage, float apDamage)
    {
        if(minion.minionData.Value.minionHealthData.ReduceHealth(adDamage + apDamage) <= 0) Die();
    }

    public void Die()
    {
        minion.GetComponent<NetworkObject>().Despawn();
    }
}
