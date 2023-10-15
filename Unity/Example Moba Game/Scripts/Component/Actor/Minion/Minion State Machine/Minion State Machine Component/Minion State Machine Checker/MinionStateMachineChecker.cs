using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStateMachineChecker
{
    private Minion minion;

    public SingleRangeChecker singleRangeChecker;

    public MinionStateMachineChecker(Minion minion, MinionStateMachine minionStateMachine)
    {
        this.minion = minion;
        singleRangeChecker = new SingleRangeChecker(minion, minion.minionData.Value.minionAttackData.minionAttackRange);
    }
    
    public bool CheckEnemyTargets() => singleRangeChecker.CheckEnemyTargets();
    
    public bool CheckAttackCooldown()
    {
        if (Time.time >= minion.minionData.Value.minionAttackData.minionLastAttackTime + minion.minionData.Value.minionAttackData.minionAttackCooldown) return true;
        return false;
    }

    public bool CheckEnemyTeam() 
    {
        if (minion.team != singleRangeChecker.target.team) return true;
        singleRangeChecker.target = null;
        return false;
    }
}
