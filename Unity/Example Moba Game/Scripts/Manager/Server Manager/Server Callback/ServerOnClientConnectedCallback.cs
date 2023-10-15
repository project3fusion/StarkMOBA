using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerOnClientConnectedCallback : ServerCallback
{
    public override void InitializeCallback() => NetworkManager.Singleton.OnClientConnectedCallback += (id) => Callback((int) id);

    public override void Callback(int id)
    {
        if (!ServerManager.Instance.IsServer) return;
        ServerManager.Instance.IncreasePlayerCount();
    }
}
