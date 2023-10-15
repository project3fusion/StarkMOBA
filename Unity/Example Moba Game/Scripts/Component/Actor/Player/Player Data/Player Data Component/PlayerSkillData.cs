using Unity.Netcode;

public struct PlayerSkillData : INetworkSerializable
{
    public enum SkillType { AD, AP }
    public int ID;
    public SkillType skillType;
    public float radius;
    public float range;
    public float attackDamage;
    public float armor;
    public float startTime;
    public float endTime;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ID);
        serializer.SerializeValue(ref skillType);
        serializer.SerializeValue(ref radius);
        serializer.SerializeValue(ref range);
        serializer.SerializeValue(ref attackDamage);
        serializer.SerializeValue(ref armor);
        serializer.SerializeValue(ref startTime);
        serializer.SerializeValue(ref endTime);
    }
}
