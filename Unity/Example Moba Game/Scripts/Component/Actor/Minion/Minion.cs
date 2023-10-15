using Unity.Netcode;
using UnityEngine;

public class Minion : Actor
{
    public MinionSettings minionSettings;

    public NetworkVariable<MinionData> minionData = new NetworkVariable<MinionData>();

    public MinionStateMachine minionStateMachine;

    public MinionAnimator minionAnimator;
    public MinionAttack minionAttack;
    public MinionCoroutine minionCoroutine;
    public MinionEvent minionEvent;
    public MinionMovement minionMovement;
    public MinionRotation minionRotation;
    public MinionUI minionUI;
    public MinionVFX minionVFX;

    public NetworkObject minionNetworkObject;

    public bool isReady;

    private void Start() => StartCoroutine((minionCoroutine = new MinionCoroutine(this)).minionAwaitSetupCoroutine.Coroutine());

    public override void OnNetworkSpawn() => minionNetworkObject = GetComponent<NetworkObject>();

    public override void OnDataGenerated()
    {
        id = minionData.Value.minionID;
        team = (Team) minionData.Value.minionTeam;
        type = Type.Minion;
    }

    public override void RecieveDamage(float adDamage, float apDamage) => minionEvent.RecieveDamage(adDamage, apDamage);

    public override void SetSelfTargetPointTransform() => selfTargetPointTransform = ServerManager.Instance.GenerateGameObject("Target Point", transform).transform;

    public override void SendDamage(float adDamage, float apDamage, Actor target, Transform myTransform, string key) => minionEvent.SendDamage(adDamage, apDamage, target, myTransform, key);

    private void Update()
    {
        if (!isReady) return;
        if (IsClient) minionUI.OnUpdate();
        if (isDead.Value) return;
        if (IsClient) minionAnimator.OnUpdate();
    }

    public void OnOptimizedUpdate()
    {
       if (!isDead.Value) minionStateMachine.OnOptimizedUpdate();
    }

    public void OnRespawn()
    {
        if (IsServer) isDead.Value = false;
        if (IsServer) minionMovement.OnRespawn();
        if (IsServer) ServerManagerOptimizedUpdate.optimizedMinionQueue.Enqueue(this);
        ToggleUIClientRpc();
    }

    public void OnDeath()
    {
        MinionDeathAnimationOrderClientRpc();
        ToggleUIClientRpc();
    }

    [ClientRpc] public void HandleHitVFXClientRpc(Vector3 position, Quaternion rotation) => minionVFX.PlayVFX(minionSettings.hitVFX, position, rotation, 1f);
    [ClientRpc]
    public void MinionAttackAnimationOrderClientRpc()
    {
        if(minionAnimator != null) minionAnimator.PlayAttackAnimation("Normal Attack");
    }
    [ClientRpc] public void MinionDeathAnimationOrderClientRpc() => minionAnimator.PlayAnimation("Death");
    [ClientRpc] public void ToggleUIClientRpc() => minionUI.ToggleUI();
}
