using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMovement
{
    private Minion minion;

    public Vector3 desiredDestination;
    public NavMeshAgent agent;
    public Transform target;

    public MinionMovement(Minion minion) => this.minion = minion;

    public void OnStart()
    {
        Setup();
        SetDestination();
    }

    public void OnRespawn()
    {
        SetDestination();
    }

    public void Setup()
    {
        minion.gameObject.AddComponent<NavMeshAgent>();
        agent = minion.gameObject.GetComponent<NavMeshAgent>();
        agent.angularSpeed = minion.minionSettings.angularSpeed;
        agent.acceleration = minion.minionSettings.acceleration;
        agent.stoppingDistance = minion.minionSettings.stoppingDistance;
        agent.autoBraking = minion.minionSettings.autoBraking;
    }

    public void SetDestination()
    {
        agent.ResetPath();
        agent.SetDestination(new Vector3(
            minion.minionData.Value.minionMovementData.desiredMovementDestination.x,
            0,
            minion.minionData.Value.minionMovementData.desiredMovementDestination.y
        ));
        agent.isStopped = true;
    }

    public void StopMovement(Transform target)
    {
        this.target = target;
        agent.isStopped = true;
        minion.minionData.Value.minionAnimationData.UpdateState(MinionAnimationData.MinionAnimationState.Idle);
    }

    public void StopMovement() => agent.isStopped = true;

    public void StartMovement()
    {
        target = null;
        SmoothRotateToMovementDirection();
        agent.isStopped = false;
        minion.minionData.Value.minionAnimationData.UpdateState(MinionAnimationData.MinionAnimationState.Run);
    }

    private void SmoothRotateToMovementDirection()
    {
        Vector3 direction = agent.velocity.normalized;
        direction.y = 0f;
        if (direction != Vector3.zero) minion.transform.rotation = Quaternion.Slerp(minion.transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);
    }
}
