using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManagerCoroutine
{
    public static void StartCoroutines()
    {
        ServerManager.Instance.StartCoroutine(ServerManagerTowerStarterCoroutine.Coroutine());
    }
}
