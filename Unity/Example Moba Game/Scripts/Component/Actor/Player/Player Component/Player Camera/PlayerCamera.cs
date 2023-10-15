using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerCamera
{
    private Player player;
    private Transform cameraTransform;
    private Vector3 offset;
    private float smoothSpeed;
    private bool isCameraFollowingPlayer = true;

    public PlayerCamera(Player player)
    {
        this.player = player;
        offset = player.playerSettings.playerCameraOffset;
        smoothSpeed = player.playerSettings.playerCameraMoveSpeed;
    }

    public void OnStart() => (cameraTransform = Camera.main.transform).position = Player.Owner.transform.position + offset;

    public void OnUpdate()
    {
        if (player.playerInput.cameraInput == Vector2.zero) isCameraFollowingPlayer = true;
        else isCameraFollowingPlayer = false;
        if (isCameraFollowingPlayer) cameraTransform.position = Player.Owner.transform.position + offset;
    }

    public void OnLateUpdate()
    {
        if (Player.Owner != null)
        {
            Vector3 desiredPosition = cameraTransform.position + new Vector3(player.playerInput.cameraInput.x, 0, player.playerInput.cameraInput.y);
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed);
            cameraTransform.position = smoothedPosition;
        }
    }
}
