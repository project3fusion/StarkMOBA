using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MinionAttack
{
    private Minion minion;
    private Actor target;

    public MinionAttack(Minion minion)
    {
        this.minion = minion;
    }

    public void AttackTarget() {
        minion.minionMovement.target = minion.minionStateMachine.minionStateMachineChecker.singleRangeChecker.target.transform;
        minion.SendDamage(5, 0, minion.minionStateMachine.minionStateMachineChecker.singleRangeChecker.target, minion.transform, "Minion Projectile " + minion.team.ToString());
        minion.minionData.Value.minionAttackData.UpdateData(Time.time);
        minion.MinionAttackAnimationOrderClientRpc();
    }
}
