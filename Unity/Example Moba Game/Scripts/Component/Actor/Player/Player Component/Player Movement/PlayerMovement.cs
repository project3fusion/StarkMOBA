using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Unity.Collections;
using UnityEngine.Experimental.AI;
using Unity.VisualScripting;

[System.Serializable]
public class PlayerMovement
{
    private Player player;

    public NavMeshAgent navmeshAgent;
    private RaycastHit hit;
    private Vector3 direction;
    private float playerRotationSpeed;
    private string terrainLayerName;

    public PlayerMovement(Player player)
    {
        this.player = player;
        playerRotationSpeed = player.playerSettings.playerRotationSpeed;
        terrainLayerName = player.playerSettings.terrainLayerName;
    }

    public void OnStart()
    {
        navmeshAgent = player.AddComponent<NavMeshAgent>();
        navmeshAgent.angularSpeed = player.playerSettings.angularSpeed;
        navmeshAgent.acceleration = player.playerSettings.acceleration;
        navmeshAgent.stoppingDistance = player.playerSettings.stoppingDistance;
        navmeshAgent.autoBraking = player.playerSettings.autoBraking;
        navmeshAgent.destination = player.transform.position;
    }

    public void OnUpdate()
    {
        if (player.IsOwner && player.playerInput.RightClick)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, Mathf.Infinity, LayerMask.GetMask(terrainLayerName))) ClientTryMovementRequest(hit.point);
    }

    public void ClientTryMovementRequest(Vector3 movePosition) => player.PlayerMovementRequestServerRpc(new Vector2(movePosition.x, movePosition.z));

    public void ServerTryMove(Transform targetTransform) => player.playerData.Value.playerMovementData.UpdateData(new Vector2(targetTransform.position.x, targetTransform.position.z), Time.time, isMoveRequested: true, isMoving: true);

    public void ServerStartCheckingDistance() => player.playerData.Value.playerMovementData.UpdateData(isPlayerMoveRequested: false, isPlayerMoving: true);

    public void StartMovement()
    {
        navmeshAgent.SetDestination(new Vector3(player.playerData.Value.playerMovementData.playerMovementDestination.x, 0, player.playerData.Value.playerMovementData.playerMovementDestination.y));
        navmeshAgent.isStopped = false;
        player.playerData.Value.playerAnimationData.UpdateData(PlayerAnimationData.PlayerAnimationState.Run);
    }

    public void SmoothRotate()
    {
        direction = navmeshAgent.velocity.normalized;
        direction.y = 0f;
        if(direction != Vector3.zero) player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(direction), playerRotationSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        navmeshAgent.isStopped = true;
        player.playerData.Value.playerMovementData.UpdateData(isPlayerMoveRequested: false, isPlayerMoving: false);
        player.playerData.Value.playerAnimationData.UpdateData(PlayerAnimationData.PlayerAnimationState.Idle);
    }
}
