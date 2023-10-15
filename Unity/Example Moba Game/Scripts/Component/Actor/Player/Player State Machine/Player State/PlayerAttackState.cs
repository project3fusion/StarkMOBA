using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override PlayerState HandleState()
    {
        player.playerAttack.ServerTryAttackTarget();
        return playerStateMachine.playerAttackStanceState;
    }
}
