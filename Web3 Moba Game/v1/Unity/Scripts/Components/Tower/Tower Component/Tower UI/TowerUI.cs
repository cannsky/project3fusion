using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUI
{
    private Tower tower;

    private TMP_Text towerHealthText;

    public TowerUI(Tower tower) => this.tower = tower;

    public void OnStart()
    {
        towerHealthText = tower.towerSettings.towerHealthText;
    }

    public void OnUpdate()
    {
        UpdateTowerUI();
        SyncronizeTextWithCamera(towerHealthText.transform);
    }

    public void UpdateTowerUI()
    {
        Debug.Log(tower.towerData.Value.towerHealthData.towerHealth);
        towerHealthText.text = tower.towerData.Value.towerHealthData.towerHealth + " / " + tower.towerData.Value.towerHealthData.towerTotalHealth;
    }

    private void SyncronizeTextWithCamera(Transform transform) => transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
}
