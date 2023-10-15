using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineChecker
{
    private Player player;
    private PlayerStateMachine playerStateMachine;

    public PlayerStateMachineChecker(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public bool CheckPlayerMoveRequested() => player.playerData.Value.playerMovementData.isPlayerMoveRequested;

    public bool CheckIsPlayerMoving() => player.playerData.Value.playerMovementData.isPlayerMoving;

    public bool CheckIsPlayerReachedDestination() => player.playerMovement.navmeshAgent.remainingDistance < 0.1f;

    public bool CheckPlayerAttackRequested() => player.playerData.Value.playerAttackData.isPlayerAttackRequested;

    public bool CheckIsPlayerAttacking() => player.playerData.Value.playerAttackData.isPlayerAttacking;

    public bool CheckIsPlayerTargetNull() => player.playerAttack.ServerGetTarget() == null;

    public bool CheckIsPlayerTargetDead() => player.playerAttack.ServerCheckIsTargetDead();

    public bool CheckPlayerTargetRange() => player.playerAttack.ServerCheckTargetRange();

    public bool CheckPlayerAttackCooldown() => player.playerAttack.ServerCheckPlayerAttackCooldown();

    public bool CheckPlayerTargetTeam() => player.playerAttack.ServerCheckTargetTeam();
}
