using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override PlayerState HandleState()
    {
        if (playerStateMachine.playerStateMachineChecker.CheckPlayerAttackRequested() ||
            playerStateMachine.playerStateMachineChecker.CheckIsPlayerAttacking())
            return playerStateMachine.playerAttackStanceState;
        else if (playerStateMachine.playerStateMachineChecker.CheckIsPlayerMoving()) return UpdatePlayerMovement();
        else if (playerStateMachine.playerStateMachineChecker.CheckPlayerMoveRequested()) return StartPlayerMovement();
        else return StopPlayerMovement();
    }

    public PlayerState StartPlayerMovement()
    {
        player.playerMovement.StartMovement();
        ServerManager.Instance.StartCoroutine(EnableCheckPlayerReachedDestination());
        return this;
    }

    public PlayerState StopPlayerMovement()
    {
        player.playerMovement.StopMovement();
        return playerStateMachine.playerIdleState;
    }

    public PlayerState UpdatePlayerMovement()
    {
        if (playerStateMachine.playerStateMachineChecker.CheckIsPlayerReachedDestination() && !playerStateMachine.playerStateMachineChecker.CheckPlayerMoveRequested()) return StopPlayerMovement();
        player.playerMovement.SmoothRotate();
        return this;
    }

    public IEnumerator EnableCheckPlayerReachedDestination()
    {
        yield return null;
        if (player.playerMovement.navmeshAgent.remainingDistance > 0) player.playerMovement.ServerStartCheckingDistance();
        else ServerManager.Instance.StartCoroutine(EnableCheckPlayerReachedDestination());
    }
}
