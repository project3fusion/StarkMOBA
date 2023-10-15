using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn
{
    private Player player;

    public PlayerSpawn(Player player) => this.player = player;

    public void OnStart() => Spawn();

    public void Spawn()
    {
        player.isDead.Value = false;
        player.playerData.Value.playerHealthData.FillHealth();
        player.playerData.Value.playerManaData.FillMana();
        player.playerData.Value.playerAnimationData.UpdateData(PlayerAnimationData.PlayerAnimationState.Idle);
        player.PlayerAnimationOrderClientRpc("Idle");
        player.transform.position = GamePosition.GetRandomPosition(GamePosition.Type.TeamSpawn, (GamePosition.Team)player.playerData.Value.playerTeam);
        player.playerMovement.navmeshAgent.isStopped = true;
        player.playerMovement.navmeshAgent.enabled = false;
        player.playerMovement.navmeshAgent.enabled = true;
    }
}
