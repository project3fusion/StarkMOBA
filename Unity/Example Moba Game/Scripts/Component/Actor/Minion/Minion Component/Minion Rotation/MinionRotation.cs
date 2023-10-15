using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionRotation
{
    private Minion minion;

    public MinionRotation(Minion minion) => this.minion = minion;

    public void SmoothRotateToTarget()
    {
        Vector3 direction = minion.minionStateMachine.minionStateMachineChecker.singleRangeChecker.target.transform.position - minion.transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero) minion.transform.rotation = Quaternion.Slerp(minion.transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);
    }
}
