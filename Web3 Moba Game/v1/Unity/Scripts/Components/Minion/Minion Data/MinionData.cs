using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MinionData : INetworkSerializable
{
    public enum PlayerTeam { Blue, Red, Neutral }

    public MinionAttackData minionAttackData;
    public MinionHealthData minionHealthData;
    public MinionMovementData minionMovementData;

    public PlayerTeam playerTeam;
    public int minionID;
    public bool isSet;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerTeam);
        serializer.SerializeValue(ref minionID);
        serializer.SerializeValue(ref isSet);
        minionAttackData.NetworkSerialize(serializer);
        minionHealthData.NetworkSerialize(serializer);
        minionMovementData.NetworkSerialize(serializer);
    }
}
