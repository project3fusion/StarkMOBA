using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    public enum Team { Blue, Red, Neutral }
    public enum SpawnedObjectType { Minion }

    public SpawnedObjectType spawnedObjectType;
    public Team team;

    public SpawnerSettings spawnerSettings;

    public GameObject minionPrefab;
    public Transform enemySpawner;

    public float cooldown = 25f;

    private void Start() => StartCoroutine(SpawnMinions());

    private IEnumerator SpawnMinions()
    {
        while (true)
        {
            if (!IsServer) yield return new WaitForSeconds(2f);
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.5f);
                    NetworkObject minionNetworkObject = ServerManager.Instance.serverManagerNetworkObjectPools["Minion " + team.ToString()].GetNetworkObject(transform.position, Quaternion.identity);
                    Minion minion = minionNetworkObject.gameObject.GetComponent<Minion>();
                    minion.minionData.Value = new MinionData(minion.id, (MinionData.MinionTeam)team, minion.minionSettings, enemySpawner.position);
                    minion.OnDataGenerated();
                    ServerManager.Instance.AddMinion(minion, (int) team);
                    if(minion.isDead.Value) minion.OnRespawn();
                }
                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}
