using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn
{
    private Player player;

    public PlayerSpawn(Player player) => this.player = player;

    public void Spawn()
    {
        player.transform.position = GamePosition.GetRandomPosition(GamePosition.Type.TeamSpawn, (GamePosition.Team)player.playerData.Value.playerTeam);
        player.playerMovement.agent.isStopped = true;
        player.playerMovement.agent.enabled = false;
        player.playerMovement.agent.enabled = true;
    }
}
