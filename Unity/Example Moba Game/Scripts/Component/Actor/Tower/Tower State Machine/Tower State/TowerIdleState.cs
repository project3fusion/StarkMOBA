using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIdleState : TowerState
{
    public TowerIdleState(Tower tower, TowerStateMachine towerStateMachine) : base(tower, towerStateMachine) { }

    public override TowerState HandleState()
    {
        if (towerStateMachine.towerStateMachineChecker.CheckEnemyTargets()) return towerStateMachine.towerAttackStanceState;
        else return this;
    }
}
