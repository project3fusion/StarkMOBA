using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerState
{
    public Tower tower;
    public TowerStateMachine towerStateMachine;

    public TowerState(Tower tower, TowerStateMachine towerStateMachine)
    {
        this.tower = tower;
        this.towerStateMachine = towerStateMachine;
    }

    public abstract TowerState HandleState();
}
