using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Pool;

public class ServerManagerNetworkObjectPool
{
    public GameObject prefab;
    public Transform poolTransform;
    public ObjectPool<NetworkObject> objectPool;
    public List<NetworkObject> poolGameObjects;
    public int defaultCapacity;

    public ServerManagerNetworkObjectPool(Transform poolTransform, ServerManagerNetworkObjectPoolSettings serverManagerNetworkObjectPoolSettings)
    {
        prefab = serverManagerNetworkObjectPoolSettings.prefab;
        defaultCapacity = serverManagerNetworkObjectPoolSettings.defaultCapacity;
        this.poolTransform = poolTransform;
        InitializePool();
    }

    public ServerManagerNetworkObjectPool(Transform poolTransform, GameObject prefab, int defaultCapacity)
    {
        this.prefab = prefab;
        this.defaultCapacity = defaultCapacity;
        this.poolTransform = poolTransform;
        InitializePool();
    }

    public void InitializePool()
    {
        objectPool = new ObjectPool<NetworkObject>(CreateObject, ActivateObject, DeactivateObject, DestroyObject, defaultCapacity: defaultCapacity);
        poolGameObjects = new List<NetworkObject>();
        for (int i = 0; i < defaultCapacity; i++) poolGameObjects.Add(objectPool.Get());
        foreach (var networkObject in poolGameObjects) objectPool.Release(networkObject);
        NetworkManager.Singleton.PrefabHandler.AddHandler(prefab, new ServerManagerPoolPrefabHandler(this));
    }

    private NetworkObject CreateObject()
    {
        GameObject instantiatedGameObject = ServerManager.Instance.InstantiateGameObject(prefab, Vector3.zero, Quaternion.identity, poolTransform);
        UpdateServerManagerLists(instantiatedGameObject);
        return instantiatedGameObject.GetComponent<NetworkObject>();
    }
    private void ActivateObject(NetworkObject networkObject) => networkObject.gameObject.SetActive(true);
    private void DeactivateObject(NetworkObject networkObject) => networkObject.gameObject.SetActive(false);
    private void DestroyObject(NetworkObject networkObject) => ServerManager.Instance.DestroyGameObject(networkObject.gameObject);

    public void ReturnNetworkObject(NetworkObject networkObject)
    {
        objectPool.Release(networkObject);
    }

    public NetworkObject GetNetworkObject(Vector3 position, Quaternion rotation)
    {
        var networkObject = objectPool.Get();
        networkObject.transform.position = position;
        networkObject.transform.rotation = rotation;
        networkObject.Spawn();
        networkObject.TrySetParent(poolTransform);
        return networkObject;
    }

    public void UpdateServerManagerLists(GameObject instantiatedGameObject)
    {
        if (instantiatedGameObject.TryGetComponent(out Minion minion))
        {
            ServerManager.Instance.minions.Add(minion);
            minion.id = ServerManager.Instance.minions.Count - 1;
        }
    }
}
