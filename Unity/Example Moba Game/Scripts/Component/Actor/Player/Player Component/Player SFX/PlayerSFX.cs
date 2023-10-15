using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX
{
    private Player player;

    public PlayerSFX(Player player) => this.player = player;

    public void PlaySFX(GameObject gameObject, Vector3 position, Quaternion rotation, float destroySeconds)
        => player.StartCoroutine(DestroyVFX(player.InstantiateGameObject(gameObject, position, rotation), destroySeconds));

    public IEnumerator DestroyVFX(GameObject vfxGameObject, float destroySeconds)
    {
        yield return new WaitForSeconds(destroySeconds);
        player.DestroyGameObject(vfxGameObject);
    }
}
