using Unity.Netcode;

public class PlayerData : INetworkSerializable
{
    public enum PlayerTeam { Blue, Red, Neutral }

    public PlayerAnimationData playerAnimationData;
    public PlayerArmorData playerArmorData;
    public PlayerAttackData playerAttackData;
    public PlayerChampionData playerChampionData;
    public PlayerDamageData playerDamageData;
    public PlayerEquipmentData playerEquipmentData;
    public PlayerHealthData playerHealthData;
    public PlayerManaData playerManaData;
    public PlayerMovementData playerMovementData;

    public PlayerTeam playerTeam;
    public int playerID;
    public bool isSet;

    public PlayerData() 
    {
        playerChampionData = new PlayerChampionData();
        playerAnimationData = new PlayerAnimationData();
        playerArmorData = new PlayerArmorData();
        playerAttackData = new PlayerAttackData();
        playerDamageData = new PlayerDamageData();
        playerEquipmentData = new PlayerEquipmentData();
        playerHealthData = new PlayerHealthData();
        playerManaData = new PlayerManaData();
        playerMovementData = new PlayerMovementData();
    }

    public PlayerData(Champion champion)
    {
        playerChampionData = new PlayerChampionData(champion);
        playerAnimationData = new PlayerAnimationData();
        playerAttackData = new PlayerAttackData();
        playerEquipmentData = new PlayerEquipmentData();
        playerMovementData = new PlayerMovementData();
        playerArmorData = new PlayerArmorData(champion);
        playerDamageData = new PlayerDamageData(champion);
        playerHealthData = new PlayerHealthData(champion);
        playerManaData = new PlayerManaData(champion);
        isSet = true;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerID);
        serializer.SerializeValue(ref isSet);
        serializer.SerializeValue(ref playerTeam);
        playerAnimationData.NetworkSerialize(serializer);
        playerArmorData.NetworkSerialize(serializer);
        playerAttackData.NetworkSerialize(serializer);
        playerDamageData.NetworkSerialize(serializer);
        playerManaData.NetworkSerialize(serializer);
        playerMovementData.NetworkSerialize(serializer);
        playerEquipmentData.NetworkSerialize(serializer);
    }
}
