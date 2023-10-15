using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackState : TowerState
{
    public TowerAttackState(Tower tower, TowerStateMachine towerStateMachine) : base(tower, towerStateMachine) { }

    public override TowerState HandleState()
    {
        tower.towerAttack.AttackTarget();
        return towerStateMachine.towerAttackStanceState;
    }
}
