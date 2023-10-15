using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXPool
{
    private Player player;

    private Transform parent;
    private GameObject prefab;
    private List<GameObject> pool;
    private int poolSize = 3;

    public PlayerVFXPool(Player player, GameObject prefab, Transform parent)
    {
        this.player = player;
        this.prefab = prefab;
        this.parent = parent;
        pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++) GetFromPool();
        foreach (GameObject poolGameObject in pool) poolGameObject.SetActive(false);
    }

    public void PlayVFX(Vector3 position, Quaternion rotation, float destroySeconds)
    {
        GameObject vfxObject = GetFromPool();
        vfxObject.transform.position = position;
        vfxObject.transform.rotation = rotation;
        var particleSystem = vfxObject.GetComponent<ParticleSystem>();
        particleSystem.Stop();
        particleSystem.Play();
        player.StartCoroutine(ReturnToPool(vfxObject, destroySeconds));
    }

    private GameObject GetFromPool()
    {
        foreach (var pooledObject in pool)
        {
            if (!pooledObject.activeInHierarchy)
            {
                pooledObject.SetActive(true);
                return pooledObject;
            }
        }
        GameObject newObject = player.InstantiateGameObject(prefab, Vector3.zero, Quaternion.identity);
        pool.Add(newObject);
        newObject.transform.parent = parent;
        return newObject;
    }

    private IEnumerator ReturnToPool(GameObject vfxObject, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        vfxObject.GetComponent<ParticleSystem>().Stop();
        vfxObject.SetActive(false);
    }
}