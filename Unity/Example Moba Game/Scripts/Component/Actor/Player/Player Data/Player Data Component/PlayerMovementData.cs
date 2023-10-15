using Unity.Netcode;
using UnityEngine;

public class PlayerMovementData : INetworkSerializable
{
    public Vector2 playerMovementDestination;
    public float playerMovementTime;
    public bool isPlayerMoveRequested, isPlayerMoving;

    public PlayerMovementData()
    {
        playerMovementDestination = Vector2.zero;
        playerMovementTime = 0;
        isPlayerMoveRequested = isPlayerMoving = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerMovementDestination);
        serializer.SerializeValue(ref playerMovementTime);
        serializer.SerializeValue(ref isPlayerMoveRequested);
        serializer.SerializeValue(ref isPlayerMoving);
    }

    public void UpdateData(bool isPlayerMoveRequested = false, bool isPlayerMoving = false)
    {
        this.isPlayerMoveRequested = isPlayerMoveRequested;
        this.isPlayerMoving = isPlayerMoving;
    }

    public void UpdateData(Vector2 playerMovementDestination, float playerMovementTime = -1, bool isMoveRequested = false, bool isMoving = false)
    {
        this.playerMovementDestination = playerMovementDestination;
        this.playerMovementTime = playerMovementTime < 0 ? this.playerMovementTime : playerMovementTime;
        this.isPlayerMoveRequested = isMoveRequested;
        this.isPlayerMoving = isMoving;
    }
}
