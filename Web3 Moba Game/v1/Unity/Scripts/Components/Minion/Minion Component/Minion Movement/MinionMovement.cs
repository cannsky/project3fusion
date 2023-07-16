using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMovement
{
    private Minion minion;

    public Vector3 desiredDestination;
    public NavMeshAgent agent;
    public bool isTargetDetected;
    public Transform target;

    public MinionMovement(Minion minion) => this.minion = minion;

    public void OnStart()
    {
        if (!minion.IsServer) return;
        agent = minion.GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(
            minion.minionData.Value.minionMovementData.desiredMovementDestination.x, 
            0, 
            minion.minionData.Value.minionMovementData.desiredMovementDestination.y
        ));
        minion.minionData.Value.minionAnimationData.minionAnimationState = MinionAnimationData.MinionAnimationState.Run;
    }

    public void OnUpdate()
    {
        if (!minion.IsServer) return;
        if (isTargetDetected && target == null)
        {
            agent.isStopped = false;
            minion.minionData.Value.minionAnimationData.UpdateState(MinionAnimationData.MinionAnimationState.Idle);
        }
        else if (isTargetDetected)
        {
            SmoothRotate();
            agent.isStopped = true;
            minion.minionData.Value.minionAnimationData.UpdateState(MinionAnimationData.MinionAnimationState.Idle);
        }
        else
        {
            agent.isStopped = false;
            minion.minionData.Value.minionAnimationData.UpdateState(MinionAnimationData.MinionAnimationState.Run);
        }
    }

    private void SmoothRotate()
    {
        Vector3 direction = target.position - minion.transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero) minion.transform.rotation = Quaternion.Slerp(minion.transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);
    }
}
