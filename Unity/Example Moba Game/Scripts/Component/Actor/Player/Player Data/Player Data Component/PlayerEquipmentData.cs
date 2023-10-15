using Unity.Netcode;

public class PlayerEquipmentData : INetworkSerializable
{
    public PlayerItemData[] playerItems = new PlayerItemData[5];
    public PlayerRuneData[] playerRunes = new PlayerRuneData[5];

    public PlayerEquipmentData()
    {
        for(int i = 0; i < playerItems.Length; i++) playerItems[i] = new PlayerItemData();
        for(int i = 0;i < playerRunes.Length; i++) playerRunes[i] = new PlayerRuneData();
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        for (int i = 0; i < playerItems.Length; i++) playerItems[i].NetworkSerialize(serializer);
        for (int i = 0; i < playerRunes.Length; i++) playerRunes[i].NetworkSerialize(serializer);
    }
}
