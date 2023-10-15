using Unity.Netcode;

public class PlayerManaData : INetworkSerializable
{
    public enum SumType { TotalMana, ManaRegenerationSpeed }

    public float playerMana;
    public float playerTotalMana;
    public float playerManaRegenerationSpeed;

    public PlayerManaData()
    {
        playerMana = 0;
        playerTotalMana = 0;
        playerManaRegenerationSpeed = 0;
    }

    public PlayerManaData(Champion champion)
    {
        playerMana = champion.totalMana - 50;
        playerTotalMana = champion.totalMana;
        playerManaRegenerationSpeed = champion.manaRegenerationSpeed;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerMana);
        serializer.SerializeValue(ref playerTotalMana);
        serializer.SerializeValue(ref playerManaRegenerationSpeed);
    }

    public void UpdateData(float playerMana = -1)
    {
        this.playerMana = playerMana < 0 ? this.playerMana : playerMana;
    }

    public void UpdateData(float playerTotalMana = -1, float playerManaRegenerationSpeed = -1)
    {
        this.playerTotalMana = playerTotalMana < 0 ? this.playerTotalMana : playerTotalMana;
        this.playerManaRegenerationSpeed = playerManaRegenerationSpeed < 0 ? this.playerManaRegenerationSpeed : playerManaRegenerationSpeed;
    }

    public void FillMana() => playerMana = playerTotalMana;

    public void RegenerateMana() => playerMana = playerMana >= playerTotalMana ? playerTotalMana : playerMana + playerManaRegenerationSpeed;
}
