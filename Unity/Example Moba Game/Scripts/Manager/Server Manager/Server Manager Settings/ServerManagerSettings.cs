using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerManagerSettings
{
    [Header("Server Manager Network Object Pools")]
    public List<ServerManagerNetworkObjectPoolSettings> serverManagerNetworkObjectPoolSettings;
}

[System.Serializable]
public class ServerManagerNetworkObjectPoolSettings
{
    public string key;
    public GameObject prefab;
    public int defaultCapacity;
}
