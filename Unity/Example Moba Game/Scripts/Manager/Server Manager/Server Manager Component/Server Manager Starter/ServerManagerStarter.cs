using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManagerStarter
{
    public static UnityTransport unityTransport;

    public static void Start()
    {
        if (!ServerManagerArguments.isServerStart) return;
        unityTransport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
        unityTransport.ConnectionData.Port = (ushort) ServerManagerArguments.serverPort;
        unityTransport.ConnectionData.Address = "127.0.0.1";
        Application.targetFrameRate = 30;
        NetworkManager.Singleton.StartServer();
    }
}
