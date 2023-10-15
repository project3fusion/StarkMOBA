using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override PlayerState HandleState()
    {
        if (playerStateMachine.playerStateMachineChecker.CheckPlayerMoveRequested()) return playerStateMachine.playerRunState;
        else if (playerStateMachine.playerStateMachineChecker.CheckPlayerAttackRequested()) return playerStateMachine.playerAttackStanceState;
        else return this;
    }
}
