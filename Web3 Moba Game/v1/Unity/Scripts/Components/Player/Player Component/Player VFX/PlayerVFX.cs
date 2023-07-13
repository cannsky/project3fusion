using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX
{
    private Player player;

    public PlayerVFX(Player player) => this.player = player;

    public void PlayVFX(GameObject gameObject, Vector3 position, Quaternion rotation, float destroySeconds) 
        => player.StartCoroutine(DestroyVFX(player.InstantiateGameObject(gameObject, position, rotation), destroySeconds));

    public IEnumerator DestroyVFX(GameObject vfxGameObject, float destroySeconds)
    {
        yield return new WaitForSeconds(destroySeconds);
        player.DestroyGameObject(vfxGameObject);
    }
}
