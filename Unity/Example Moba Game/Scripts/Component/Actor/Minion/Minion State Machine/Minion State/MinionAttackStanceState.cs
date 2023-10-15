using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackStanceState : MinionState
{
    public MinionAttackStanceState(Minion minion, MinionStateMachine minionStateMachine) : base(minion, minionStateMachine) { }

    public override MinionState HandleState()
    {
        if (!minionStateMachine.minionStateMachineChecker.CheckAttackCooldown()) return Wait();
        if (minionStateMachine.minionStateMachineChecker.CheckEnemyTargets())
        {
            minion.minionMovement.StopMovement(minionStateMachine.minionStateMachineChecker.singleRangeChecker.target.transform);
            if (minionStateMachine.minionStateMachineChecker.CheckEnemyTeam()) return minionStateMachine.minionAttackState;
            else return minionStateMachine.minionRunState;
        }
        else return minionStateMachine.minionRunState;
    }

    public MinionState Wait()
    {
        minion.minionRotation.SmoothRotateToTarget();
        return this;
    }
}
