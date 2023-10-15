using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor
{
    private Player player;
    private RaycastHit hit;
    private GameObject playerCursorVFX;
    private string terrainLayerName;

    private bool isRightClickCalled;

    public PlayerCursor(Player player)
    {
        this.player = player;
        terrainLayerName = player.playerSettings.terrainLayerName;
        playerCursorVFX = player.playerSettings.playerCursorVFX;
    }

    public void OnUpdate()
    {
        if (player.playerInput.RightClick && !isRightClickCalled)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 
                out hit, Mathf.Infinity, LayerMask.GetMask(terrainLayerName))) CallVFX();
    }

    public void CallVFX()
    {
        isRightClickCalled = true;
        player.playerVFX.PlayMarkerVFX(new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), playerCursorVFX.transform.rotation, 0.5f);
    }

    public void OnLateUpdate() => isRightClickCalled = player.playerInput.RightClick ? true : false;
}
