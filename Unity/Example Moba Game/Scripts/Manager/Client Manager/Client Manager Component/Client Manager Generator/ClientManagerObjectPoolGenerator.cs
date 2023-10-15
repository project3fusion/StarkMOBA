using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagerObjectPoolGenerator
{
    private ClientManager clientManager;

    public ClientManagerObjectPoolGenerator(ClientManager clientManager) => this.clientManager = clientManager;

    public void AddPlayerSpecificPoolGameObject(GameObject prefab, string key, int defaultCapacity)
    {
        Transform poolTransform = GameObject.Find("Client Manager Player Pool").transform;
        clientManager.clientManagerPools.Add(key, new ClientManagerGameObjectPool(poolTransform, prefab, defaultCapacity));
    }
}
