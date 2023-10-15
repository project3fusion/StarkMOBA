using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinionState
{
    public Minion minion;
    public MinionStateMachine minionStateMachine;

    public MinionState(Minion minion, MinionStateMachine minionStateMachine)
    {
        this.minion = minion;
        this.minionStateMachine = minionStateMachine;
    }

    public abstract MinionState HandleState();
}
