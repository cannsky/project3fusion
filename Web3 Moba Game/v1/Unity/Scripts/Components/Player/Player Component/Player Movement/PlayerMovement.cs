using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PlayerMovement
{
    private Player player;

    public NavMeshAgent agent;
    private RaycastHit hit;
    private Vector3 direction;
    private float playerRotationSpeed;
    private string terrainLayerName;

    public PlayerMovement(Player player)
    {
        this.player = player;
        playerRotationSpeed = player.playerSettings.playerRotationSpeed;
        terrainLayerName = player.playerSettings.terrainLayerName;
    }

    public void OnStart()
    {
        if (player.IsServer)
        {
            player.AddComponent<NavMeshAgent>();
            agent = player.GetComponent<NavMeshAgent>();
            agent.angularSpeed = player.playerSettings.angularSpeed;
            agent.acceleration = player.playerSettings.acceleration;
            agent.stoppingDistance = player.playerSettings.stoppingDistance;
            agent.autoBraking = player.playerSettings.autoBraking;
            agent.destination = player.transform.position;
            if (player.IsHost) player.playerSpawn = new PlayerSpawn(player);
            player.playerSpawn.Spawn();
        }
    }

    public void OnUpdate()
    {
        if (player.IsOwner && player.playerInput.RightClick)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, Mathf.Infinity, LayerMask.GetMask(terrainLayerName))) ClientTryMovementRequest(hit.point);
        ServerTryMove();
        ClientVisuals();
    }

    public void ClientTryMovementRequest(Vector3 movePosition)
    {
        if (!player.IsOwner) return;
        if (!player.IsClient) return;
        player.PlayerMovementRequestServerRpc(new Vector2(movePosition.x, movePosition.z));
    }

    public void ServerTryMove()
    {
        if (!player.IsServer) return;
        SmoothRotate();
        if (!player.playerData.Value.playerMovementData.isMoving) StopMovement();
        if (!player.playerData.Value.playerMovementData.isMoveRequested && player.playerData.Value.playerMovementData.isMoving && agent.remainingDistance < 0.1f) StopMovement();
        if (!player.playerData.Value.playerMovementData.isMoveRequested) return;
        agent.SetDestination(new Vector3(player.playerData.Value.playerMovementData.playerMovementDestination.x, 0, player.playerData.Value.playerMovementData.playerMovementDestination.y));
        agent.isStopped = false;
        player.playerData.Value.playerMovementData.UpdateData(isMoveRequested: false, isMoving: true);
        player.playerData.Value.playerAnimationData.UpdateData(PlayerAnimationData.PlayerAnimationState.Run);
    }

    public void ServerTryMove(Transform targetTransform)
    {
        player.playerData.Value.playerMovementData.UpdateData(new Vector2(targetTransform.position.x, targetTransform.position.z), Time.time, isMoveRequested: true, isMoving: true);
    }

    private void SmoothRotate()
    {
        direction = agent.velocity.normalized;
        direction.y = 0f;
        if(direction != Vector3.zero) player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(direction), playerRotationSpeed * Time.deltaTime);
    }

    private void ClientVisuals()
    {
        if (!player.IsClient) return;
        if (player.playerData.Value.playerAnimationData.playerAnimationState == PlayerAnimationData.PlayerAnimationState.Idle) player.playerAnimator.PlayRunAnimation(false);
        else if (player.playerData.Value.playerAnimationData.playerAnimationState == PlayerAnimationData.PlayerAnimationState.Run) player.playerAnimator.PlayRunAnimation(true);
        else if (player.playerData.Value.playerAnimationData.playerAnimationState == PlayerAnimationData.PlayerAnimationState.Attack) player.playerAnimator.PlayAttackAnimation("Normal Attack");
    }

    public void StopMovement()
    {
        agent.isStopped = true;
        player.playerData.Value.playerMovementData.UpdateData(isMoveRequested: false, isMoving: false);
        player.playerData.Value.playerAnimationData.UpdateData(PlayerAnimationData.PlayerAnimationState.Idle);
    }
}
