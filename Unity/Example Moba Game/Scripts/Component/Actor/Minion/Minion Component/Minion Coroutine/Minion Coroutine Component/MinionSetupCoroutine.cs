using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSetupCoroutine
{
    private Minion minion;

    public MinionSetupCoroutine(Minion minion) => this.minion = minion;

    public IEnumerator Coroutine()
    {
        while (true)
        {
            if (minion == null) break;
            if (minion != null && minion.minionData != null && minion.minionData.Value != null)
            {
                if (minion.minionData.Value.isSet)
                {
                    Setup();
                    break;
                }
                else yield return null;
            }
            else yield return null;
        }
    }

    public void Setup()
    {
        if (minion.IsClient) ClientSetup();
        if (minion.IsServer) ServerSetup();
        minion.isReady = true;
    }

    public void ClientSetup()
    {
        minion.minionAnimator = new MinionAnimator(minion);
        minion.minionUI = new MinionUI(minion);
        minion.minionVFX = new MinionVFX(minion);
        minion.minionAnimator.OnStart();
        minion.minionUI.OnStart();
    }

    public void ServerSetup()
    {
        minion.minionStateMachine = new MinionStateMachine(minion);
        minion.minionAttack = new MinionAttack(minion);
        minion.minionEvent = new MinionEvent(minion);
        minion.minionMovement = new MinionMovement(minion);
        minion.minionRotation = new MinionRotation(minion);
        ServerManagerOptimizedUpdate.optimizedMinionQueue.Enqueue(minion);
        minion.minionMovement.OnStart();
        minion.SetSelfTargetPointTransform();
    }
}
