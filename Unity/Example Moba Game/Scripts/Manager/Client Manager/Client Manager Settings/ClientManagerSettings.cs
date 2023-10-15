using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientManagerSettings
{
    [Header("Client Manager Background Music")]
    public List<AudioClip> gameAudioClips;
    public List<AudioClip> lobbyAudioClips;
}
