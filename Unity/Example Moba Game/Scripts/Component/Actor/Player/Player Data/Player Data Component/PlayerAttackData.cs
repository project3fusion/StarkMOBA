using Unity.Netcode;

public class PlayerAttackData : INetworkSerializable
{
    public enum TargetType { Player, Minion, Tower, Null }
    public TargetType playerTargetType;
    public int playerTargetID;
    public float playerLastAttackTime;
    public bool isPlayerAttackRequested, isPlayerAttacking;

    public PlayerAttackData()
    {
        playerTargetID = 0;
        playerLastAttackTime = 0;
        isPlayerAttacking = false;
        isPlayerAttackRequested = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerTargetType);
        serializer.SerializeValue(ref playerTargetID);
        serializer.SerializeValue(ref playerLastAttackTime);
        serializer.SerializeValue(ref isPlayerAttacking);
        serializer.SerializeValue(ref isPlayerAttackRequested);
    }

    public void SetAttackRequested(int playerTargetID, TargetType playerTargetType)
    {
        this.playerTargetID = playerTargetID;
        this.playerTargetType = playerTargetType;
        isPlayerAttackRequested = true;
    }

    public void SetAttackSequenceData(int playerTargetID, TargetType playerTargetType)
    {
        this.playerTargetID = playerTargetID;
        this.playerTargetType = playerTargetType;
        isPlayerAttacking = true;
    }

    public void StopAttackSequenceData()
    {
        isPlayerAttacking = false;
    }

    public void UpdateData(bool isPlayerAttacking = false)
    {
        this.isPlayerAttacking = isPlayerAttacking;
    }

    public void UpdateData(int playerTargetID = 0, bool isPlayerAttacking = false)
    {
        this.playerTargetID = playerTargetID;
        this.isPlayerAttacking = isPlayerAttacking;
    }

    public void UpdateData(float playerLastAttackTime = 0)
    {
        this.playerLastAttackTime = playerLastAttackTime;
    }

    public void UpdateData(int playerTargetID = 0, float playerLastAttackTime = 0, bool isPlayerAttacking = false)
    {
        this.playerTargetID = playerTargetID;
        this.playerLastAttackTime = playerLastAttackTime;
        this.isPlayerAttacking = isPlayerAttacking;
    }
}
