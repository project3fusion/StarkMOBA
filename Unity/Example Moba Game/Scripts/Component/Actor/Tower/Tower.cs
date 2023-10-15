using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Tower : Actor
{
    public TowerSettings towerSettings;

    public NetworkVariable<TowerData> towerData = new NetworkVariable<TowerData>();

    public TowerStateMachine towerStateMachine;

    public TowerAttack towerAttack;
    public TowerCoroutine towerCoroutine;
    public TowerEvent towerEvent;
    public TowerUI towerUI;

    public bool isReady;

    public override void OnDataGenerated()
    {
        id = towerData.Value.towerID;
        team = (Team) towerData.Value.towerTeam;
        type = Type.Tower;
    }

    public override void RecieveDamage(float adDamage, float apDamage) => towerEvent.RecieveDamage(adDamage, apDamage);

    public override void SetSelfTargetPointTransform() => selfTargetPointTransform = ServerManager.Instance.GenerateGameObject("Target Point", transform).transform;

    public override void SendDamage(float adDamage, float apDamage, Actor target, Transform myTransform, string key) => towerEvent.SendDamage(adDamage, apDamage, target, myTransform, key);

    private void Start()
    {
        if (IsServer) GenerateTowerData();
        StartCoroutine((towerCoroutine = new TowerCoroutine(this)).towerAwaitSetupCoroutine.Coroutine());
    }

    private void Update()
    {
        if (!isReady || isDead.Value) return;
        if (IsClient) towerUI.OnUpdate();
    }

    public void OnOptimizedUpdate()
    {
        if (!IsServer || isDead.Value) return;
        towerStateMachine.OnOptimizedUpdate();
        ServerManagerOptimizedUpdate.optimizedTowerQueue.Enqueue(this);
    }

    private void GenerateTowerData()
    {
        towerData.Value = new TowerData(id, towerSettings);
        OnDataGenerated();
    }
}
