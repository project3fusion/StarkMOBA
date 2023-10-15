using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MinionData : INetworkSerializable
{
    public enum MinionTeam { Blue, Red, Neutral }

    public MinionAnimationData minionAnimationData;
    public MinionAttackData minionAttackData;
    public MinionHealthData minionHealthData;
    public MinionMovementData minionMovementData;

    public MinionTeam minionTeam;
    public int minionID;
    public bool isSet;

    public MinionData()
    {
        minionTeam = MinionTeam.Blue;
        minionID = 0;
        isSet = false;
        minionAnimationData = new MinionAnimationData();
        minionAttackData = new MinionAttackData();
        minionHealthData = new MinionHealthData();
        minionMovementData = new MinionMovementData();
    }

    public MinionData(int id)
    {
        minionID = id;
        minionAnimationData = new MinionAnimationData();
        minionAttackData = new MinionAttackData();
        minionHealthData = new MinionHealthData();
        minionMovementData = new MinionMovementData();
        isSet = true;
    }

    public MinionData(int id, MinionTeam minionTeam, MinionSettings minionSettings, Vector3 destination)
    {
        this.minionTeam = minionTeam;
        minionID = id;
        minionAnimationData = new MinionAnimationData();
        minionAttackData = new MinionAttackData(minionSettings);
        minionHealthData = new MinionHealthData();
        minionMovementData = new MinionMovementData(destination);
        isSet = true;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref minionTeam);
        serializer.SerializeValue(ref minionID);
        serializer.SerializeValue(ref isSet);
        minionAnimationData.NetworkSerialize(serializer);
        minionAttackData.NetworkSerialize(serializer);
        minionHealthData.NetworkSerialize(serializer);
        minionMovementData.NetworkSerialize(serializer);
    }
}
