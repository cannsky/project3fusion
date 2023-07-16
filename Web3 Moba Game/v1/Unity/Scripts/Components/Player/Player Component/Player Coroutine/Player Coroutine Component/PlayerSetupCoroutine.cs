using System.Collections;
using UnityEngine;

public class PlayerSetupCoroutine
{
    private Player player;

    public PlayerSetupCoroutine(Player player) => this.player = player;

    public IEnumerator Coroutine()
    {
        while (true)
        {
            if (player == null) break;
            if (player.playerData != null && 
                player.playerData.Value != null)
            {
                if (player.playerData.Value.isSet && player.playerData.Value.playerChampionData.isChampionSet)
                {
                    Setup();
                    break;
                }
                else yield return null;
            }
            else yield return null;
        }
    }

    public void Setup()
    {
        if (player.IsClient) ClientSetup();
        if (player.IsServer) ServerSetup();
        if (player.IsOwner) OwnerSetup();
        player.isReady = true;
    }

    public void ClientSetup()
    {
        GameObject championGameObject = player.InstantiateChampionPrefab();
        championGameObject.transform.localPosition = Vector3.zero;
        player.GetComponent<Animator>().runtimeAnimatorController = championGameObject.GetComponent<Animator>().runtimeAnimatorController;
        player.GetComponent<Animator>().avatar = championGameObject.GetComponent<Animator>().avatar;
        player.DestroyGameObject(championGameObject.GetComponent<Animator>());
        player.playerAnimator = new PlayerAnimator(player);
        player.playerAttack = new PlayerAttack(player);
        player.playerMovement = new PlayerMovement(player);
        player.playerUI = new PlayerUI(player);
        player.playerVFX = new PlayerVFX(player);
        player.playerAnimator.OnStart();
        player.playerMovement.OnStart();
        player.playerUI.OnStart();
    }

    public void ServerSetup()
    {
        player.playerSpawn = new PlayerSpawn(player);
        player.playerEvent = new PlayerEvent(player);
        player.playerAttack = new PlayerAttack(player);
        player.playerMovement = new PlayerMovement(player);
        player.playerMovement.OnStart();
        player.playerCoroutine.OnStart();
    }

    public void OwnerSetup()
    {
        Player.Owner = player;
        player.playerCamera = new PlayerCamera(player);
        player.playerCursor = new PlayerCursor(player);
        player.playerInput = new PlayerInput(player);
        player.playerCamera.OnStart();
        player.playerInput.OnStart();
    }
}
