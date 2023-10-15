using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerOnClientDisconnectCallback : ServerCallback
{
    public override void InitializeCallback() => NetworkManager.Singleton.OnClientDisconnectCallback += (id) => Callback((int) id);

    public override void Callback(int id)
    {
        if (!ServerManager.Instance.IsServer) return;
        ServerManager.Instance.DecreasePlayerCount();
        ServerManager.Instance.players[id] = null;
    }
}
