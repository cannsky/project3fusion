using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinionUI
{
    private Minion minion;

    private TMP_Text minionHealthText;

    public MinionUI(Minion minion) => this.minion = minion;

    public void OnStart()
    {
        minionHealthText = minion.minionSettings.minionHealthText;
    }

    public void OnUpdate()
    {
        UpdateMinionUI();
        SyncronizeTextWithCamera(minionHealthText.transform);
    }

    public void UpdateMinionUI()
    {
        minionHealthText.text = minion.minionData.Value.minionHealthData.minionHealth + " / " + minion.minionData.Value.minionHealthData.minionTotalHealth;
    }

    private void SyncronizeTextWithCamera(Transform transform) => transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
}
