using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStanceState : PlayerState
{
    public PlayerAttackStanceState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override PlayerState HandleState()
    {
        if (!playerStateMachine.playerStateMachineChecker.CheckIsPlayerAttacking() ||
            !playerStateMachine.playerStateMachineChecker.CheckPlayerTargetTeam() ||
            playerStateMachine.playerStateMachineChecker.CheckIsPlayerTargetNull() ||
            playerStateMachine.playerStateMachineChecker.CheckIsPlayerTargetDead())
            return StopAttackSequence();
        else if (playerStateMachine.playerStateMachineChecker.CheckPlayerTargetRange())
            return MoveTowardsTarget();
        else return AttackOrWait();
    }

    public PlayerState StopAttackSequence()
    {
        player.playerData.Value.playerAttackData.StopAttackSequenceData();
        return playerStateMachine.playerIdleState;
    }

    public PlayerState MoveTowardsTarget()
    {
        player.playerMovement.ServerTryMove(player.playerAttack.ServerGetTarget().transform);
        return playerStateMachine.playerRunState;
    }

    public PlayerState AttackOrWait()
    {
        player.playerMovement.StopMovement();
        if (!playerStateMachine.playerStateMachineChecker.CheckPlayerAttackCooldown())
            return playerStateMachine.playerAttackStanceState;
        else return playerStateMachine.playerAttackState;
    }
}
