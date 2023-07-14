using Unity.Netcode;
using UnityEngine;

public class MinionMovementData : INetworkSerializable
{
    public Vector2 movementDestination, desiredMovementDestination;

    public MinionMovementData()
    {
        desiredMovementDestination = Vector2.zero;
        movementDestination = Vector2.zero;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref movementDestination);
    }

    public void SetMovementDestination(Vector2 movementDestination) => this.movementDestination = movementDestination;
}
