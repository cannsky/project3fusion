using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMovement
{
    private Minion minion;

    public Vector3 desiredDestination;
    public NavMeshAgent agent;

    public MinionMovement(Minion minion)
    {

    }

    public void OnStart()
    {
        agent.SetDestination(desiredDestination);
    }

    public void OnUpdate()
    {

    }
}
