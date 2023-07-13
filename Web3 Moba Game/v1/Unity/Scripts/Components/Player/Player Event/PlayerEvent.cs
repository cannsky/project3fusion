using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent
{
    private Player player;

    public PlayerEvent(Player player) => this.player = player;

    public void ApplyDamage(float adDamage, float apDamage)
    {
        if(player.playerData.Value.playerHealthData.ReduceHealth(adDamage + apDamage) <= 0) Die();
    }

    public void Die()
    {
        player.playerData.Value.playerHealthData.FillHealth();
        player.playerData.Value.playerManaData.FillMana();
        player.playerSpawn.Spawn();
    }
}
