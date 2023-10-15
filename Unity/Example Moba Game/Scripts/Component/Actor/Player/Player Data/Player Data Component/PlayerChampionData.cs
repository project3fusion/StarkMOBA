using Unity.Netcode;
using UnityEngine;

public class PlayerChampionData : INetworkSerializable
{
    public enum ChampionType { Melee, Ranged }
    public enum DamageType { AD, AP }

    public int ID;
    public ChampionType championType;
    public DamageType damageType;
    public float range;
    public bool isChampionSet;

    public PlayerChampionData() { }

    public PlayerChampionData(Champion networkChampion)
    {
        ID = networkChampion.ID;
        championType = (ChampionType) networkChampion.championType;
        damageType = (DamageType) networkChampion.damageType;
        range = networkChampion.range;
        isChampionSet = true;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ID);
        serializer.SerializeValue(ref championType);
        serializer.SerializeValue(ref damageType);
        serializer.SerializeValue(ref range);
        serializer.SerializeValue(ref isChampionSet);
    }
}
