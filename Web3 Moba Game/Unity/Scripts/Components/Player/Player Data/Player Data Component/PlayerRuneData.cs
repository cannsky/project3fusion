using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerRuneData : INetworkSerializable
{
    public enum Type { AD, AP }
    public int ID;
    public Type type;
    public float extraTotalHealth;
    public float extraHealthRegenerationSpeed;
    public float extraHealthStoleMultiplier;
    public float extraTotalMana;
    public float extraManaRegenerationSpeed;
    public float extraAttackSpeed;
    public float extraADAttackDamage;
    public float extraAPAttackDamage;
    public float extraADArmor;
    public float extraAPArmor;
    public float extraADArmorPiercing;
    public float extraAPArmorPiercing;
    public float extraMovementSpeed;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ID);
        serializer.SerializeValue(ref type);
        serializer.SerializeValue(ref extraTotalHealth);
        serializer.SerializeValue(ref extraHealthRegenerationSpeed);
        serializer.SerializeValue(ref extraHealthStoleMultiplier);
        serializer.SerializeValue(ref extraTotalMana);
        serializer.SerializeValue(ref extraManaRegenerationSpeed);
        serializer.SerializeValue(ref extraAttackSpeed);
        serializer.SerializeValue(ref extraADAttackDamage);
        serializer.SerializeValue(ref extraAPAttackDamage);
        serializer.SerializeValue(ref extraADArmor);
        serializer.SerializeValue(ref extraAPArmor);
        serializer.SerializeValue(ref extraADArmorPiercing);
        serializer.SerializeValue(ref extraAPArmorPiercing);
        serializer.SerializeValue(ref extraMovementSpeed);
    }
}
