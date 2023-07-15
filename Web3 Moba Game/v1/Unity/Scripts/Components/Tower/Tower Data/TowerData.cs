using Unity.Netcode;

public class TowerData : INetworkSerializable
{
    public enum TowerTeam { Blue, Red, Neutral }

    public int towerID;
    public TowerTeam towerTeam;
    public bool isSet;

    public TowerAttackData towerAttackData;
    public TowerHealthData towerHealthData;

    public TowerData()
    {
        towerAttackData = new TowerAttackData();
        towerHealthData = new TowerHealthData();
        isSet = false;
    }

    public TowerData(TowerSettings towerSettings)
    {
        towerAttackData = new TowerAttackData(towerSettings);
        towerHealthData = new TowerHealthData(towerSettings);
        isSet = true;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref isSet);
        towerAttackData.NetworkSerialize(serializer);
        towerHealthData.NetworkSerialize(serializer);
    }
}
