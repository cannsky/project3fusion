using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegenerationCoroutine
{
    private Player player;

    public PlayerRegenerationCoroutine(Player player) => this.player = player;

    public IEnumerator Coroutine()
    {
        while (true)
        {
            RegenerateHealth();
            RegenerateMana();
            yield return new WaitForSeconds(1f);
        }
    }

    public void RegenerateHealth() => player.playerData.Value.playerHealthData.RegenerateHealth();

    public void RegenerateMana() => player.playerData.Value.playerManaData.RegenerateMana();
}
