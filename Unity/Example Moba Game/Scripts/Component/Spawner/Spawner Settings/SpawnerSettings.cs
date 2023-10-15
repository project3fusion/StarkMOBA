using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerSettings
{
    [Header("Spawner Pooler")]
    public GameObject spawnerMinionPrefab;
    public int spawnerPoolSize = 25;
}
