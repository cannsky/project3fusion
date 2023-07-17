using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX
{
    private Player player;

    private GameObject playerVFX;

    public PlayerVFX(Player player) => this.player = player;

    public void OnStart() => playerVFX = player.transform.GetChild(1).GetChild(0).gameObject;

    public void PlayVFX(GameObject gameObject, Vector3 position, Quaternion rotation, float destroySeconds) 
        => player.StartCoroutine(DestroyVFX(player.InstantiateGameObject(gameObject, position, rotation), destroySeconds));

    public IEnumerator DestroyVFX(GameObject vfxGameObject, float destroySeconds)
    {
        yield return new WaitForSeconds(destroySeconds);
        player.DestroyGameObject(vfxGameObject);
    }

    public void PlayVFX() => player.StartCoroutine(DestroyVFX());

    public IEnumerator DestroyVFX()
    {
        yield return new WaitForSeconds(0.35f);
        GameObject vfxGameObject = player.InstantiateGameObject(playerVFX, playerVFX.transform.position, playerVFX.transform.rotation);
        vfxGameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        player.DestroyGameObject(vfxGameObject);
    }
}
