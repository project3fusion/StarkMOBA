using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private Player player;

    public PlayerIdleState playerIdleState;
    public PlayerRunState playerRunState;
    public PlayerAttackStanceState playerAttackStanceState;
    public PlayerAttackState playerAttackState;
    public PlayerState currentPlayerState;

    public PlayerStateMachineChecker playerStateMachineChecker;

    public PlayerStateMachine(Player player)
    {
        this.player = player;
        playerIdleState = new PlayerIdleState(player, this);
        playerRunState = new PlayerRunState(player, this);
        playerAttackStanceState = new PlayerAttackStanceState(player, this);
        playerAttackState = new PlayerAttackState(player, this);
        playerStateMachineChecker = new PlayerStateMachineChecker(player, this);
        currentPlayerState = playerIdleState;
    }

    public PlayerState OnUpdate() => currentPlayerState = currentPlayerState.HandleState();
}
