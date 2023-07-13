using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI
{
    private Player player;

    private TMP_Text playerNameText, playerHealthText, playerManaText;

    public PlayerUI(Player player) => this.player = player;

    public void OnStart()
    {
        playerNameText = player.playerSettings.playerNameText;
        playerHealthText = player.playerSettings.playerHealthText;
        playerManaText = player.playerSettings.playerManaText;
    }

    public void OnUpdate()
    {
        UpdatePlayerUI();
        SyncronizeTextWithCamera(playerNameText.transform);
        SyncronizeTextWithCamera(playerHealthText.transform);
        SyncronizeTextWithCamera(playerManaText.transform);
    }

    public void UpdatePlayerUI()
    {
        playerNameText.text = "Player " + player.playerData.Value.playerID.ToString();
        playerHealthText.text = player.playerData.Value.playerHealthData.playerHealth.ToString() + " / " + player.playerData.Value.playerHealthData.playerTotalHealth.ToString();
        playerManaText.text = player.playerData.Value.playerManaData.playerMana.ToString() + " / " + player.playerData.Value.playerManaData.playerTotalMana.ToString();
    }

    private void SyncronizeTextWithCamera(Transform transform) => transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
}
