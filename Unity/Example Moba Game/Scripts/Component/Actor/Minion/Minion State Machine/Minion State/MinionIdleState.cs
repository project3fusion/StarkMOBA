using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionIdleState : MinionState
{
    public MinionIdleState(Minion minion, MinionStateMachine minionStateMachine) : base(minion, minionStateMachine) { }

    public override MinionState HandleState()
    {
        return minionStateMachine.minionRunState;
    }
}
