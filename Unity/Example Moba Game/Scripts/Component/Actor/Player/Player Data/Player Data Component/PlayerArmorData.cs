using Unity.Netcode;

public class PlayerArmorData : INetworkSerializable
{
    public float playerADArmor;
    public float playerAPArmor;

    public PlayerArmorData()
    {
        playerADArmor = 0;
        playerAPArmor = 0;
    }

    public PlayerArmorData(Champion champion)
    {
        playerADArmor = champion.adArmor;
        playerAPArmor = champion.apArmor;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerADArmor);
        serializer.SerializeValue(ref playerAPArmor);
    }

    public void UpdateData(float playerADArmor = -1, float playerAPArmor = -1)
    {
        this.playerADArmor = playerADArmor < 0 ? this.playerADArmor : playerADArmor;
        this.playerAPArmor = playerAPArmor < 0 ? this.playerAPArmor : playerAPArmor;
    }
}
