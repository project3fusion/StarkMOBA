using Unity.Netcode;

public class PlayerDamageData : INetworkSerializable
{
    public float playerADAttackDamage;
    public float playerAPAttackDamage;
    public float playerADArmorPiercing;
    public float playerAPArmorPiercing;
    public float playerAttackCooldownTime;

    public PlayerDamageData()
    {
        playerADAttackDamage = 0;
        playerAPAttackDamage = 0;
        playerADArmorPiercing = 0;
        playerAPArmorPiercing = 0;
        playerAttackCooldownTime = 0;
    }

    public PlayerDamageData(Champion champion)
    {
        playerADAttackDamage = champion.adAttackDamage;
        playerAPAttackDamage = champion.apAttackDamage;
        playerADArmorPiercing = champion.adArmorPiercing;
        playerAPArmorPiercing = champion.apArmorPiercing;
        playerAttackCooldownTime = champion.attackCooldownTime;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerADAttackDamage);
        serializer.SerializeValue(ref playerAPAttackDamage);
        serializer.SerializeValue(ref playerADArmorPiercing);
        serializer.SerializeValue(ref playerAPArmorPiercing);
        serializer.SerializeValue(ref playerAttackCooldownTime);
    }

    //Create fields for each update.

    public void GeneratePlayerAttackData(float playerADAttackDamage = -1, float playerAPAttackDamage = -1, float playerADArmorPiercing = -1, float playerAPArmorPiercing = -1, float playerAttackCooldownTime = -1)
    {
        this.playerADAttackDamage = playerADAttackDamage < 0 ? this.playerADAttackDamage : playerADAttackDamage;
        this.playerAPAttackDamage = playerAPAttackDamage < 0 ? this.playerAPAttackDamage : playerAPAttackDamage;
        this.playerADArmorPiercing = playerADArmorPiercing < 0 ? this.playerADArmorPiercing : playerADArmorPiercing;
        this.playerAPArmorPiercing = playerAPArmorPiercing < 0 ? this.playerAPArmorPiercing : playerAPArmorPiercing;
        this.playerAttackCooldownTime = playerAttackCooldownTime < 0 ? this.playerAttackCooldownTime : playerAttackCooldownTime;
    }
}
