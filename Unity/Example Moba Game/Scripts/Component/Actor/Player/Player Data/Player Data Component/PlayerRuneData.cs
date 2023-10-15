using Unity.Netcode;

public class PlayerRuneData : INetworkSerializable
{
    public int ID;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter => serializer.SerializeValue(ref ID);
}
