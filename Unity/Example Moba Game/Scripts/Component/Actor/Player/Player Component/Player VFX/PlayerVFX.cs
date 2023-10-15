using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX
{
    private Player player;

    public PlayerVFXPool playerMarkerVFXPool, playerHitVFXPool;

    public PlayerVFX(Player player) => this.player = player;

    public void OnStart()
    {
        playerMarkerVFXPool = new PlayerVFXPool(player, player.playerSettings.playerCursorVFX, GameObject.Find("Client Manager Marker VFX").transform);
        playerHitVFXPool = new PlayerVFXPool(player, player.playerSettings.playerHitVFX, GameObject.Find("Client Manager Hit VFX").transform);
    }

    public void PlayMarkerVFX(Vector3 position, Quaternion rotation, float destroySeconds) 
        => playerMarkerVFXPool.PlayVFX(position, rotation, destroySeconds);

    public void PlayHitVFX(Vector3 position, Quaternion rotation, float destroySeconds)
    => playerHitVFXPool.PlayVFX(position, rotation, destroySeconds);
}
