using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ServerManagerObjectPoolGenerator
{
    public static Transform serverManagerPoolsParent;
    public static List<Transform> serverManagerPools;
    private static string prefix = "Server Manager ", suffix = " Pool", parentName = "Server Manager Pools";

    public static void OnStart() => GenerateServerManagerPools();

    public static void GenerateServerManagerPools()
    {
        serverManagerPoolsParent = GenerateServerManagerPoolGameObject(ServerManager.Instance.transform, parentName);
        foreach(ServerManagerNetworkObjectPoolSettings settings in ServerManager.Instance.serverManagerSettings.serverManagerNetworkObjectPoolSettings)
        {
            Transform poolTransform = GenerateServerManagerPoolGameObject(serverManagerPoolsParent, prefix + settings.key + suffix);
            ServerManager.Instance.serverManagerNetworkObjectPools.Add(settings.key, new ServerManagerNetworkObjectPool(poolTransform, settings));
        }
    }

    public static Transform GenerateServerManagerPoolGameObject(Transform parent, string name)
    {
        Transform transform = GameObject.Find(name).transform;
        return transform;
    }

    public static void AddPlayerSpecificPoolGameObject(GameObject prefab, string key, int defaultCapacity)
    {
        Transform poolTransform = GameObject.Find("Server Manager Player Pool").transform;
        ServerManager.Instance.serverManagerNetworkObjectPools.Add(key, new ServerManagerNetworkObjectPool(poolTransform, prefab, defaultCapacity));
    }
}
