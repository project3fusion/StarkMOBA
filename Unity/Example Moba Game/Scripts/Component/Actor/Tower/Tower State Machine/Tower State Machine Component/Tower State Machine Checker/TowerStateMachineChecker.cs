using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStateMachineChecker
{
    private Tower tower;

    public SingleRangeChecker singleRangeChecker;

    public TowerStateMachineChecker(Tower tower, TowerStateMachine towerStateMachine)
    {
        this.tower = tower;
        singleRangeChecker = new SingleRangeChecker(tower, tower.towerData.Value.towerAttackData.towerAttackRange);
    }

    public bool CheckEnemyTargets() => singleRangeChecker.CheckEnemyTargets();
}
