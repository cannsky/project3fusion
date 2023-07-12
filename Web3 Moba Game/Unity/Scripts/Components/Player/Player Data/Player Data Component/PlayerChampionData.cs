using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[System.Serializable]
public class PlayerChampionData : INetworkSerializable
{
    public enum ChampionType { Melee, Ranged }
    public enum DamageType { AD, AP }

    public int ID;
    public ChampionType championType;
    public DamageType damageType;
    public float range;
    public float attackCooldownTime;
    public float adAttackDamage;
    public float apAttackDamage;
    public float totalHealth;
    public float healthRegenerationSpeed;
    public float healthStoleMultiplier;
    public float totalMana;
    public float manaRegenerationSpeed;
    public float adArmor;
    public float apArmor;
    public float adArmorPiercing;
    public float apArmorPiercing;
    public float movementSpeed;

    public PlayerChampionData() { }

    public PlayerChampionData(Champion networkChampion)
    {
        ID = networkChampion.ID;
        championType = (ChampionType) networkChampion.championType;
        damageType = (DamageType) networkChampion.damageType;
        range = networkChampion.range;
        attackCooldownTime = networkChampion.attackCooldownTime;
        adAttackDamage = networkChampion.adAttackDamage;
        apAttackDamage = networkChampion.apAttackDamage;
        totalHealth = networkChampion.totalHealth;
        healthRegenerationSpeed = networkChampion.healthRegenerationSpeed;
        healthStoleMultiplier = networkChampion.healthStoleMultiplier;
        totalMana = networkChampion.totalMana;
        manaRegenerationSpeed = networkChampion.manaRegenerationSpeed;
        adArmor = networkChampion.adArmor;
        apArmor = networkChampion.apArmor;
        adArmorPiercing = networkChampion.adArmorPiercing;
        apArmorPiercing = networkChampion.apArmorPiercing;
        movementSpeed = networkChampion.movementSpeed;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ID);
        serializer.SerializeValue(ref championType);
        serializer.SerializeValue(ref damageType);
        serializer.SerializeValue(ref range);
        serializer.SerializeValue(ref attackCooldownTime);
        serializer.SerializeValue(ref adAttackDamage);
        serializer.SerializeValue(ref apAttackDamage);
        serializer.SerializeValue(ref totalHealth);
        serializer.SerializeValue(ref healthRegenerationSpeed);
        serializer.SerializeValue(ref healthStoleMultiplier);
        serializer.SerializeValue(ref totalMana);
        serializer.SerializeValue(ref manaRegenerationSpeed);
        serializer.SerializeValue(ref adArmor);
        serializer.SerializeValue(ref apArmor);
        serializer.SerializeValue(ref adArmorPiercing);
        serializer.SerializeValue(ref apArmorPiercing);
        serializer.SerializeValue(ref movementSpeed);
    }
}
