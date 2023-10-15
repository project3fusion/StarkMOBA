using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionVFX
{
    private Minion minion;

    public MinionVFX(Minion minion) => this.minion = minion;

    public void PlayVFX(GameObject gameObject, Vector3 position, Quaternion rotation, float destroySeconds)
        => ClientManager.Instance.StartCoroutine(DestroyVFX(ClientManager.Instance.InstantiateGameObject(gameObject, position, rotation), destroySeconds));

    public IEnumerator DestroyVFX(GameObject vfxGameObject, float destroySeconds)
    {
        yield return new WaitForSeconds(destroySeconds);
        ClientManager.Instance.DestroyGameObject(vfxGameObject);
    }
}
