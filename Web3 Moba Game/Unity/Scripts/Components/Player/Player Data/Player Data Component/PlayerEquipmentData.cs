using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerEquipmentData : INetworkSerializable
{
    public PlayerItemData[] playerItems = new PlayerItemData[5];
    public PlayerRuneData[] playerRunes = new PlayerRuneData[5];

    public PlayerEquipmentData()
    {
        playerItems = new PlayerItemData[5];
        playerRunes = new PlayerRuneData[5];
        for (int i = 0; i < 5; i++) playerItems[i] = new PlayerItemData();
        for (int i = 0; i < 5; i++) playerRunes[i] = new PlayerRuneData();
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        playerItems = new PlayerItemData[5];
        playerRunes = new PlayerRuneData[5];
        for (int i = 0; i < 5; i++) playerItems[i] = new PlayerItemData();
        for (int i = 0; i < 5; i++) playerRunes[i] = new PlayerRuneData();
        for (int i = 0; i < 5; i++) playerItems[i].NetworkSerialize(serializer);
        for (int i = 0; i < 5; i++) playerRunes[i].NetworkSerialize(serializer);
    }
}
