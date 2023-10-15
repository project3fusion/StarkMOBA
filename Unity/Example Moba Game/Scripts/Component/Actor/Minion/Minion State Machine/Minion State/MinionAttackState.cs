using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackState : MinionState
{
    public MinionAttackState(Minion minion, MinionStateMachine minionStateMachine) : base(minion, minionStateMachine) { }

    public override MinionState HandleState()
    {
        minion.minionAttack.AttackTarget();
        return minionStateMachine.minionAttackStanceState;
    }
}
