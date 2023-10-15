using UnityEngine;

[System.Serializable]
public class PlayerInput
{
    private Player player;

    public Vector2 cameraInput;
    public bool LeftClick, RightClick, Q, W, E, R, Space;

    public PlayerInputActions playerInputActions;

    public PlayerInput(Player player) => this.player = player;

    public void OnStart()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.LeftClick.performed += i => LeftClick = true;
        playerInputActions.Movement.RightClick.performed += i => RightClick = true;
        playerInputActions.Camera.CameraReset.performed += i => Space = true;
        playerInputActions.Camera.CameraMovement.performed += i => cameraInput = i.ReadValue<Vector2>();
        playerInputActions.Enable();
    }

    public void OnLateUpdate() => LeftClick = RightClick = Q = W = E = R = Space = false;
}
