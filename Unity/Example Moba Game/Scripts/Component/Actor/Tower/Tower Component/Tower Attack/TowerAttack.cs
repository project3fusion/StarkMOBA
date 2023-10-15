using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TowerAttack
{
    private Tower tower;

    public TowerAttack(Tower tower) => this.tower = tower;

    public void AttackTarget()
    {
        tower.SendDamage(10, 0, tower.towerStateMachine.towerStateMachineChecker.singleRangeChecker.target, tower.transform, "Tower Projectile " + tower.team.ToString());
        tower.towerData.Value.towerAttackData.UpdateData(Time.time);
    }
}
