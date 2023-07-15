using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class MinionSettings
{
    [Header("Minion Attack")]
    public float attackCooldown = 3;
    [Header("Minion Health")]
    public float totalHealth = 20;
    [Header("Minion UI")]
    public TMP_Text minionHealthText;
}
