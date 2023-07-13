using System.Collections;
using System.Collections.Generic;

public class PlayerCoroutine
{
    public Player player;

    public PlayerAwaitSetupCoroutine playerAwaitSetupCoroutine;
    public PlayerRegenerationCoroutine playerRegenerationCoroutine;

    public PlayerCoroutine(Player player)
    {
        this.player = player;
        playerAwaitSetupCoroutine = new PlayerAwaitSetupCoroutine(player);
        playerRegenerationCoroutine = new PlayerRegenerationCoroutine(player);
    }

    public void OnStart() => player.StartCoroutine(playerRegenerationCoroutine.Coroutine());
}