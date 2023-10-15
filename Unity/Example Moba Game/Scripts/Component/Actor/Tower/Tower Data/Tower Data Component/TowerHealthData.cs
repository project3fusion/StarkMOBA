using Unity.Netcode;

public class TowerHealthData : INetworkSerializable
{
    public float towerHealth;
    public float towerTotalHealth;

    public TowerHealthData()
    {
        towerHealth = 0;
        towerTotalHealth = 0;
    }

    public TowerHealthData(TowerSettings towerSettings)
    {
        towerHealth = towerSettings.towerTotalHealth;
        towerTotalHealth = towerSettings.towerTotalHealth;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref towerHealth);
        serializer.SerializeValue(ref towerTotalHealth);
    }

    public float ReduceHealth(float damage) => towerHealth -= damage;
}
