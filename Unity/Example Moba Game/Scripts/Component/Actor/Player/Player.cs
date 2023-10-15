using Unity.Netcode;
using UnityEngine;

public class Player : Actor
{
    public static Player Owner;

    public PlayerSettings playerSettings;

    public PlayerStateMachine playerStateMachine;

    public NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>();

    public PlayerAnimator playerAnimator;
    public PlayerAttack playerAttack;
    public PlayerCoroutine playerCoroutine;
    public PlayerCamera playerCamera;
    public PlayerCursor playerCursor;
    public PlayerEvent playerEvent;
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerSFX playerSFX;
    public PlayerSpawn playerSpawn;
    public PlayerSpeech playerSpeech;
    public PlayerUI playerUI;
    public PlayerVFX playerVFX;

    public Champion champion;
    public bool isReady;

    public override void OnNetworkSpawn()
    {
        if (IsClient) playerData.OnValueChanged += OnValueChanged;
        if (IsOwner) Owner = this;
        StartCoroutine((playerCoroutine = new PlayerCoroutine(this)).playerAwaitSetupCoroutine.Coroutine());
    }

    public override void OnDataGenerated()
    {
        id = playerData.Value.playerID;
        team = (Team) playerData.Value.playerTeam;
        type = Type.Player;
    }

    public override void RecieveDamage(float adDamage, float apDamage) => playerEvent.RecieveDamage(adDamage, apDamage);

    public override void SetSelfTargetPointTransform() => selfTargetPointTransform = ServerManager.Instance.GenerateGameObject("Target Point", transform).transform;

    public override void SendDamage(float adDamage, float apDamage, Actor target, Transform myTransform, string key) => playerEvent.SendDamage(adDamage, apDamage, target, myTransform, key);

    private void Update()
    {
        if (!isReady || isDead.Value) return;
        if (IsOwner || IsServer) playerAttack.OnUpdate();
        if (IsOwner) playerCamera.OnUpdate();
        if (IsOwner) playerCursor.OnUpdate();
        if (IsClient) playerUI.OnUpdate();
        if (IsClient) playerAnimator.OnUpdate();
        playerMovement.OnUpdate();
        if (IsServer && !isDead.Value) playerStateMachine.OnUpdate();
    }

    private void LateUpdate()
    {
        if (!isReady) return;
        if (IsOwner) playerInput.OnLateUpdate();
        if (IsOwner) playerCamera.OnLateUpdate();
        if (IsOwner) playerCursor.OnLateUpdate();
    }

    public void GeneratePlayerData(int championID)
    {
        playerData.Value = new PlayerData(this, ServerManager.Instance.players.Count, PlayerDataGenerator.GeneratePlayerChampionData(championID));
        ServerManager.Instance.players.Add(this);
        OnDataGenerated();
    }
    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);
    public void DestroyGameObject(Animator removedGameObject) => Destroy(removedGameObject);
    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public GameObject InstantiateChampionPrefab() => Instantiate(champion.characterPrefab, transform);
    public void OnValueChanged(PlayerData a, PlayerData b)
    {
        if (!isReady) return;
    }

    /*
     * SERVER RPC'S DON'T CHECK ANYTHING!
     * THIS IS TOTALLY UNSAFE!
     */

    [ClientRpc] public void HandleHitVFXClientRpc(Vector3 position, Quaternion rotation) => playerVFX.PlayHitVFX(position, rotation, 1f);
    [ClientRpc] public void PlayerDeathSpeechClientRpc() => playerSpeech.DeathSpeech();
    [ClientRpc] public void PlayerAnimationOrderClientRpc(string animationName) => playerAnimator.PlayAnimation(animationName);
    [ServerRpc] public void PlayerMovementRequestServerRpc(Vector2 playerMovementDestination)
    {
        playerData.Value.playerMovementData.UpdateData(playerMovementDestination, Time.time, isMoveRequested: true, isMoving: false);
    }
    [ServerRpc] public void PlayerAnimationStateRequestServerRpc(PlayerAnimationData.PlayerAnimationState playerAnimationState) => playerData.Value.playerAnimationData.UpdateData(playerAnimationState);
    [ServerRpc] public void PlayerAttackRequestServerRpc(int playerTargetID, PlayerAttackData.TargetType playerTargetType)
    {
        playerData.Value.playerAttackData.SetAttackSequenceData(playerTargetID, playerTargetType);
    }
    [ServerRpc]
    public void PlayerStopAttackRequestServerRpc()
    {
        playerData.Value.playerAttackData.StopAttackSequenceData();
    }
    [ServerRpc] public void PlayerSelectChampionServerRpc(int championID) => GeneratePlayerData(championID);
}
