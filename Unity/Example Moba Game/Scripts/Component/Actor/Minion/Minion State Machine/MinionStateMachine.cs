using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinionStateMachine
{
    private Minion minion;

    public MinionIdleState minionIdleState;
    public MinionRunState minionRunState;
    public MinionAttackStanceState minionAttackStanceState;
    public MinionAttackState minionAttackState;
    public MinionState currentMinionState;

    public MinionStateMachineChecker minionStateMachineChecker;

    public MinionStateMachine(Minion minion)
    {
        this.minion = minion;
        minionIdleState = new MinionIdleState(minion, this);
        minionRunState = new MinionRunState(minion, this);
        minionAttackStanceState = new MinionAttackStanceState(minion, this);
        minionAttackState = new MinionAttackState(minion, this);
        minionStateMachineChecker = new MinionStateMachineChecker(minion, this);
        currentMinionState = minionIdleState;
    }

    public void OnOptimizedUpdate() => currentMinionState = currentMinionState.HandleState();
}
