using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStateMachine
{
    private Tower tower;

    public TowerIdleState towerIdleState;
    public TowerAttackStanceState towerAttackStanceState;
    public TowerAttackState towerAttackState;
    public TowerState currentTowerState;

    public TowerStateMachineChecker towerStateMachineChecker;

    public TowerStateMachine(Tower tower)
    {
        this.tower = tower;
        towerIdleState = new TowerIdleState(tower, this);
        towerAttackStanceState = new TowerAttackStanceState(tower, this);
        towerAttackState = new TowerAttackState(tower, this);
        towerStateMachineChecker = new TowerStateMachineChecker(tower, this);
        currentTowerState = towerIdleState;
    }

    public void OnOptimizedUpdate() => currentTowerState = currentTowerState.HandleState();
}
