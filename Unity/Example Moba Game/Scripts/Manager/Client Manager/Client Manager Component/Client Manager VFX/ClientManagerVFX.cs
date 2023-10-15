using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagerVFX
{
    private ClientManager clientManager;

    public ClientManagerVFX(ClientManager clientManager)
    {
        this.clientManager = clientManager;
    }

    public void PlayVFX(string key, Vector3 vfxPosition, Quaternion vfxRotation)
    {
        clientManager.StartCoroutine(StopVFX(clientManager.clientManagerPools[key].GetGameObject(vfxPosition, vfxRotation), key));
    }

    public IEnumerator StopVFX(GameObject vfxGameObject, string key)
    {
        yield return new WaitForSeconds(1f);
        clientManager.clientManagerPools[key].ReturnGameObject(vfxGameObject);
    }
}
