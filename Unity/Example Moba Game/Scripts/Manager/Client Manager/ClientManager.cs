using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public ClientManagerBackgroundMusic clientManagerBackgroundMusic;
    public ClientManagerChampionSpeech clientManagerChampionSpeech;
    public ClientManagerGenerator clientManagerGenerator;
    public ClientManagerVFX clientManagerVFX;

    public ClientManagerSettings clientManagerSettings;

    public static ClientManager Instance;

    public Dictionary<string, ClientManagerGameObjectPool> clientManagerPools = new Dictionary<string, ClientManagerGameObjectPool>();

    public bool isReady;

    private void Awake()
    {
        if(Instance == null) DontDestroyOnLoad(Instance = this);
        else Destroy(this);
    }

    private void Start()
    {
        clientManagerBackgroundMusic = new ClientManagerBackgroundMusic(this);
        clientManagerChampionSpeech = new ClientManagerChampionSpeech(this);
        clientManagerGenerator = new ClientManagerGenerator(this);
        clientManagerVFX = new ClientManagerVFX(this);
    }

    public void StartAfterOwnerAwake()
    {
        clientManagerBackgroundMusic.OnStart();
        clientManagerBackgroundMusic.ChangeBackgroundMusicType(ClientManagerBackgroundMusic.BackgroundMusicType.GameAudio);
    }

    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);
    [ClientRpc] public void PlayCollisionVFXClientRpc(string key, Vector3 position, Quaternion rotation) => clientManagerVFX.PlayVFX(key, position, rotation);
}
