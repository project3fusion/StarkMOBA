using Unity.Netcode;

public class PlayerItemData : INetworkSerializable
{
    public int ID;

    public PlayerItemData() => ID = 0;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter => serializer.SerializeValue(ref ID);
}
