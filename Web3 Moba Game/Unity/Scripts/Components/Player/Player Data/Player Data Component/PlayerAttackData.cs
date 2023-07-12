using Unity.Netcode;

public class PlayerAttackData : INetworkSerializable
{
    public int playerTargetID;
    public float playerLastAttackTime;
    public bool isPlayerAttacking;

    public PlayerAttackData()
    {
        playerTargetID = 0;
        playerLastAttackTime = 0;
        isPlayerAttacking = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerTargetID);
        serializer.SerializeValue(ref playerLastAttackTime);
        serializer.SerializeValue(ref isPlayerAttacking);
    }

    public void UpdateData(bool isPlayerAttacking = false)
    {
        this.isPlayerAttacking = isPlayerAttacking;
    }

    public void UpdateData(int playerTargetID = 0, bool isPlayerAttacking = false)
    {
        this.playerTargetID = playerTargetID;
        this.isPlayerAttacking = isPlayerAttacking;
    }

    public void UpdateData(float playerLastAttackTime = 0, bool isPlayerAttacking = false)
    {
        this.playerLastAttackTime = playerLastAttackTime;
        this.isPlayerAttacking = isPlayerAttacking;
    }

    public void UpdateData(int playerTargetID = 0, float playerLastAttackTime = 0, bool isPlayerAttacking = false)
    {
        this.playerTargetID = playerTargetID;
        this.playerLastAttackTime = playerLastAttackTime;
        this.isPlayerAttacking = isPlayerAttacking;
    }
}
