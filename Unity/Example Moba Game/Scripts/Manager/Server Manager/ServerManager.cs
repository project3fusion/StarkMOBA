using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class ServerManager : NetworkBehaviour
{
    public static ServerManager Instance;

    public List<Actor> minions, towers, players;

    public Spawner blueSpawner, redSpawner;

    public ServerManagerSettings serverManagerSettings;

    public ServerManagerData serverManagerData;

    private NetworkVariable<int> playerCount = new NetworkVariable<int>();
    private List<ServerCallback> callbacks = new List<ServerCallback>() { new ServerOnClientConnectedCallback(), new ServerOnClientDisconnectCallback() };
    public Dictionary<string, ServerManagerNetworkObjectPool> serverManagerNetworkObjectPools = new Dictionary<string, ServerManagerNetworkObjectPool>();

    private void Awake()
    {
        if (Instance == null) DontDestroyOnLoad(Instance = this);
        else Destroy(this);
        minions = new List<Actor>();
        players = new List<Actor>();
    }

    private void Start()
    {
        foreach (ServerCallback callback in callbacks) callback.InitializeCallback();
        serverManagerData = new ServerManagerData();
        ServerManagerArguments.Get();
        ServerManagerStarter.Start();
        ServerManagerCoroutine.StartCoroutines();
    }

    private void Update()
    {
        if (!IsServer) return;
        ServerManagerRegularUpdate.RegularUpdate();
        ServerManagerOptimizedUpdate.OptimizedUpdate();
    }

    public void IncreasePlayerCount() => playerCount.Value++;
    public void DecreasePlayerCount() => playerCount.Value--;
    public GameObject GenerateGameObject(string name, Transform parent) => ServerManagerGameObjectGenerator.GenerateGameObject(name, parent.position, parent.rotation, parent);
    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation, Transform parent) => Instantiate(addedGameObject, position, rotation, parent);
    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);
    public void AddMinion(Actor minion, int team) => serverManagerData.serverManagerTeamData[team].teamMinions.Add(minion);
    public void RemoveMinion(Actor minion, int team) => serverManagerData.serverManagerTeamData[team].teamMinions.Remove(minion);
    public void AddPlayer(Actor player, int team) => serverManagerData.serverManagerTeamData[team].teamPlayers.Add(player);
    public void RemovePlayer(Actor player, int team) => serverManagerData.serverManagerTeamData[team].teamPlayers.Remove(player);
    public void AddTower(Actor tower, int team) => serverManagerData.serverManagerTeamData[team].teamTowers.Add(tower);
    public void RemoveTower(Actor tower, int team) => serverManagerData.serverManagerTeamData[team].teamTowers.Remove(tower);
}
