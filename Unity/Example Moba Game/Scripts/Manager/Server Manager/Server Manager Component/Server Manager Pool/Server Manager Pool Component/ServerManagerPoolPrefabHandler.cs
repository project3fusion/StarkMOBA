using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerManagerPoolPrefabHandler : INetworkPrefabInstanceHandler
{
    private ServerManagerNetworkObjectPool pool;

    public ServerManagerPoolPrefabHandler(ServerManagerNetworkObjectPool pool) => this.pool = pool;

    NetworkObject INetworkPrefabInstanceHandler.Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        return pool.GetNetworkObject(position, rotation);
    }
    void INetworkPrefabInstanceHandler.Destroy(NetworkObject networkObject)
    {
        pool.ReturnNetworkObject(networkObject);
    }
}
