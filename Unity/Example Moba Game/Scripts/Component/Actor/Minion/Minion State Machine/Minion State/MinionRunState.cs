using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionRunState : MinionState
{
    public MinionRunState(Minion minion, MinionStateMachine minionStateMachine) : base(minion, minionStateMachine) { }

    public override MinionState HandleState()
    {
        minion.minionMovement.StartMovement();
        if (minionStateMachine.minionStateMachineChecker.CheckEnemyTargets()) return minionStateMachine.minionAttackStanceState;
        else return this;
    }
}
