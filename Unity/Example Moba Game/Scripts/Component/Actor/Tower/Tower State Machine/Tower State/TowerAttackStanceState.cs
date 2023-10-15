using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackStanceState : TowerState
{
    public TowerAttackStanceState(Tower tower, TowerStateMachine towerStateMachine) : base(tower, towerStateMachine) { }

    public override TowerState HandleState()
    {
        if (!CheckIsAttackAvailable()) return this;
        if (towerStateMachine.towerStateMachineChecker.CheckEnemyTargets())
        {
            if (CheckIsAttackAvailable()) return towerStateMachine.towerAttackState;
            else return this;
        }
        else return towerStateMachine.towerIdleState;
    }

    private bool CheckIsAttackAvailable()
    {
        if (Time.time >= tower.towerData.Value.towerAttackData.towerLastAttackTime + tower.towerData.Value.towerAttackData.towerAttackCooldown) return true;
        return false;
    }
}
