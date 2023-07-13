using Unity.Netcode;
using UnityEngine;

public class PlayerMovementData : INetworkSerializable
{
    public Vector2 playerMovementDestination;
    public float playerMovementTime;
    public bool isMoveRequested, isMoving;

    public PlayerMovementData()
    {
        playerMovementDestination = Vector2.zero;
        playerMovementTime = 0;
        isMoveRequested = isMoving = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerMovementDestination);
        serializer.SerializeValue(ref playerMovementTime);
        serializer.SerializeValue(ref isMoveRequested);
        serializer.SerializeValue(ref isMoving);
    }

    public void UpdateData(bool isMoveRequested = false, bool isMoving = false)
    {
        this.isMoveRequested = isMoveRequested;
        this.isMoving = isMoving;
    }

    public void UpdateData(Vector2 playerMovementDestination, float playerMovementTime = -1, bool isMoveRequested = false, bool isMoving = false)
    {
        this.playerMovementDestination = playerMovementDestination;
        this.playerMovementTime = playerMovementTime < 0 ? this.playerMovementTime : playerMovementTime;
        this.isMoveRequested = isMoveRequested;
        this.isMoving = isMoving;
    }
}
