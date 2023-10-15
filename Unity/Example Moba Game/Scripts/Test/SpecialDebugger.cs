using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDebugger : MonoBehaviour
{
    public int minionID;

    public void Update()
    {
        DebugMinion(minionID);
    }

    public void DebugMinion(int id)
    {
        if (id == 0) return;
        Minion minion = (Minion) ServerManager.Instance.minions[id];
        Debug.Log("Minion Current State: " + minion.minionStateMachine.currentMinionState.ToString());
        Debug.Log("Agent remaining distance: " + minion.minionMovement.agent.remainingDistance);
    }
}
