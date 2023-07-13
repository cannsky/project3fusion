using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerSettings
{
    [Header("Player General")]
    public string playerLayerName;
    [Header("Tower Attack")]
    public GameObject towerProjectilePrefab;
    public float towerAttackRange;
    public float towerAttackCooldown;
    public float towerAttackHeight;
    [Header("Tower Health")]
    public float towerTotalHealth;
}
