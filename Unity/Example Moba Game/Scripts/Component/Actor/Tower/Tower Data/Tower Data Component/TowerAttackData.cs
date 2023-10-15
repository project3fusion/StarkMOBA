using Unity.Netcode;
using UnityEngine;

public class TowerAttackData : INetworkSerializable
{
    public float towerAttackRange;
    public float towerAttackCooldown;
    public float towerLastAttackTime;

    public TowerAttackData()
    {
        towerAttackRange = 0;
        towerAttackCooldown = 0;
        towerLastAttackTime = 0;
    }

    public TowerAttackData(TowerSettings towerSettings)
    {
        towerAttackRange = towerSettings.towerAttackRange;
        towerAttackCooldown = towerSettings.towerAttackCooldown;
        towerLastAttackTime = 0;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref towerAttackRange);
        serializer.SerializeValue(ref towerAttackCooldown);
        serializer.SerializeValue(ref towerLastAttackTime);
    }

    public void UpdateData(float towerLastAttackTime)
    {
        this.towerLastAttackTime = towerLastAttackTime;
    }
}
