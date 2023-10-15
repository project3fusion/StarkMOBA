using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MinionAnimationData : INetworkSerializable
{
    public enum MinionAnimationState { Idle, Run, Attack, Death }

    public MinionAnimationState minionAnimationState;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref minionAnimationState);
    }

    public void UpdateState(MinionAnimationState minionAnimationState) => this.minionAnimationState = minionAnimationState;
}
