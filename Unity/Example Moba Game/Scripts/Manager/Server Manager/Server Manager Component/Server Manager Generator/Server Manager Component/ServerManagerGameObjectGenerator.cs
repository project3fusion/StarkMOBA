using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManagerGameObjectGenerator
{
    public static GameObject GenerateGameObject(string name, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject newGameObject = new GameObject(name);
        newGameObject.transform.position = new Vector3(position.x, position.y + 1f, position.z);
        newGameObject.transform.rotation = rotation;
        newGameObject.transform.parent = parent;
        return newGameObject;
    }
}
