using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagerGameObjectPool
{
    public Transform parent;
    public GameObject prefab;
    public Queue<GameObject> gameObjectQueue;
    public int defaultCapacity;

    public ClientManagerGameObjectPool(Transform parent, GameObject prefab, int defaultCapacity)
    {
        this.parent = parent;
        this.prefab = prefab;
        this.defaultCapacity = defaultCapacity;
        InitializePool();
    }

    public void InitializePool()
    {
        gameObjectQueue = new Queue<GameObject>();
        for(int i = 0; i < defaultCapacity; i++)
        {
            GameObject poolGameObject = ClientManager.Instance.InstantiateGameObject(prefab, Vector3.zero, Quaternion.identity);
            poolGameObject.transform.parent = parent;
            poolGameObject.SetActive(false);
            gameObjectQueue.Enqueue(poolGameObject);
        }
    }

    public GameObject GetGameObject(Vector3 position, Quaternion rotation)
    {
        GameObject poolGameObject;
        if (gameObjectQueue.TryDequeue(out poolGameObject))
        {
            poolGameObject.transform.position = position;
            poolGameObject.transform.rotation = rotation;
            poolGameObject.SetActive(true);
            return poolGameObject;
        }
        poolGameObject = ClientManager.Instance.InstantiateGameObject(prefab, position, rotation);
        poolGameObject.transform.parent = parent;
        return poolGameObject;
    }

    public void ReturnGameObject(GameObject poolGameObject)
    {
        poolGameObject.SetActive(false);
        gameObjectQueue.Enqueue(poolGameObject);
    }
}
