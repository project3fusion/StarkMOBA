using Unity.Netcode;

public class MinionHealthData
{
    public float minionTotalHealth;
    public float minionHealth;

    public MinionHealthData()
    {
        minionTotalHealth = 20;
        minionHealth = 20;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref minionTotalHealth);
        serializer.SerializeValue(ref minionHealth);
    }

    public float ReduceHealth(float damage) => minionHealth -= damage;
}
