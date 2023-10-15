using Unity.Netcode;

public class MinionAttackData : INetworkSerializable
{
    public enum TargetType { Player, Minion, Tower }
    public TargetType minionTargetType;
    public float minionAttackCooldown;
    public float minionLastAttackTime;
    public float minionAttackRange;
    public bool isMinionAttacking;

    public MinionAttackData()
    {
        minionTargetType = TargetType.Player;
        minionAttackCooldown = 0;
        minionLastAttackTime = 0;
        minionAttackRange = 0;
        isMinionAttacking = false;
    }

    public MinionAttackData(MinionSettings minionSettings)
    {
        minionTargetType = TargetType.Player;
        minionAttackCooldown = minionSettings.attackCooldown;
        minionLastAttackTime = 0;
        minionAttackRange = 5f;
        isMinionAttacking = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref minionTargetType);
        serializer.SerializeValue(ref minionAttackCooldown);
        serializer.SerializeValue(ref minionLastAttackTime);
        serializer.SerializeValue(ref minionAttackRange);
        serializer.SerializeValue(ref isMinionAttacking);
    }

    public void UpdateData(float minionLastAttackTime)
    {
        this.minionLastAttackTime = minionLastAttackTime;
    }
}
