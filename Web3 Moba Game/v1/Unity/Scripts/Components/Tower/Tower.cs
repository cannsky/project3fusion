using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Tower : NetworkBehaviour
{
    public TowerSettings towerSettings;

    public NetworkVariable<TowerData> towerData = new NetworkVariable<TowerData>();

    public TowerAttack towerAttack;
    public TowerCoroutine towerCoroutine;
    public TowerEvent towerEvent;
    public TowerUI towerUI;

    public bool isReady;

    private void Start()
    {
        if (IsServer) GenerateTowerData();
        StartCoroutine((towerCoroutine = new TowerCoroutine(this)).towerAwaitSetupCoroutine.Coroutine());
    }

    private void Update()
    {
        if (!isReady) return;
        if (IsServer) towerAttack.OnUpdate();
        if (IsClient) towerUI.OnUpdate();
    }

    private void GenerateTowerData()
    {
        towerData.Value = new TowerData(towerSettings);
    }

    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
}
