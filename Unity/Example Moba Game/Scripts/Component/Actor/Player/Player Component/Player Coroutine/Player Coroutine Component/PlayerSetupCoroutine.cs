using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerSetupCoroutine
{
    private Player player;

    public PlayerSetupCoroutine(Player player) => this.player = player;

    public IEnumerator Coroutine()
    {
        while (true)
        {
            if (player == null) break;
            if (player.playerData != null && 
                player.playerData.Value != null)
            {
                if (player.playerData.Value.isSet && player.playerData.Value.playerChampionData.isChampionSet)
                {
                    Setup();
                    break;
                }
                else yield return null;
            }
            else yield return null;
        }
    }

    public void Setup()
    {
        if (player.IsClient) ClientSetup();
        if (player.IsServer) ServerSetup();
        if (player.IsOwner) OwnerSetup();
        player.isReady = true;
    }

    public void ClientSetup()
    {
        player.champion = (Champion) PlayerResourceFinder.Find(PlayerResourceFinder.Type.Champion, player.playerData.Value.playerChampionData.ID);
        GameObject championGameObject = player.InstantiateChampionPrefab();
        championGameObject.transform.localPosition = Vector3.zero;
        ClientManager.Instance.clientManagerGenerator.clientManagerObjectPoolGenerator.AddPlayerSpecificPoolGameObject(player.champion.normalAttackVFX.vfxHitGameObject, "Player " + player.playerData.Value.playerID + " Hit VFX", 5);
        player.GetComponent<Animator>().runtimeAnimatorController = championGameObject.GetComponent<Animator>().runtimeAnimatorController;
        player.GetComponent<Animator>().avatar = championGameObject.GetComponent<Animator>().avatar;
        player.DestroyGameObject(championGameObject.GetComponent<Animator>());
        player.playerAnimator = new PlayerAnimator(player);
        if (!player.IsHost) player.playerAttack = new PlayerAttack(player);
        if (!player.IsHost) player.playerMovement = new PlayerMovement(player);
        player.playerSpeech = new PlayerSpeech(player);
        player.playerUI = new PlayerUI(player);
        player.playerVFX = new PlayerVFX(player);
        player.playerAnimator.OnStart();
        player.playerSpeech.OnStart();
        player.playerUI.OnStart();
        player.playerVFX.OnStart();
    }

    public void ServerSetup()
    {
        Champion tempChampion = (Champion)PlayerResourceFinder.Find(PlayerResourceFinder.Type.Champion, player.playerData.Value.playerChampionData.ID);
        ServerManager.Instance.AddPlayer(player, (int)player.playerData.Value.playerTeam);
        if (player.playerData.Value.playerChampionData.championType == PlayerChampionData.ChampionType.Ranged) ServerManagerObjectPoolGenerator.AddPlayerSpecificPoolGameObject(tempChampion.normalAttackVFX.vfxGameObject, "Player " + player.playerData.Value.playerID + " Projectile", 5);
        player.playerSpawn = new PlayerSpawn(player);
        player.playerEvent = new PlayerEvent(player);
        player.playerAttack = new PlayerAttack(player);
        player.playerMovement = new PlayerMovement(player);
        player.playerStateMachine = new PlayerStateMachine(player);
        player.playerMovement.OnStart();
        player.playerSpawn.OnStart();
        player.playerCoroutine.OnStart();
        player.gameObject.name = TestScript.GenerateRandomPlayername();
        player.SetSelfTargetPointTransform();
    }

    public void OwnerSetup()
    {
        Player.Owner = player;
        player.playerCamera = new PlayerCamera(player);
        player.playerCursor = new PlayerCursor(player);
        player.playerInput = new PlayerInput(player);
        player.playerCamera.OnStart();
        player.playerInput.OnStart();
        ClientManager.Instance.StartAfterOwnerAwake();
    }
}
