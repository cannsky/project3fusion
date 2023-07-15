using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMovement
{
    private Minion minion;

    public Vector3 desiredDestination;
    public NavMeshAgent agent;

    public MinionMovement(Minion minion) => this.minion = minion;

    public void OnStart()
    {
        agent = minion.GetComponent<NavMeshAgent>();
        agent.SetDestination(desiredDestination);
    }
}
