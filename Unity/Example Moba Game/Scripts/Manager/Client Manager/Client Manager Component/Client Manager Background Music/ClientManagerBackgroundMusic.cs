using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagerBackgroundMusic
{
    private ClientManager clientManager;

    public enum BackgroundMusicType { GameAudio, LobbyAudio }
    public BackgroundMusicType backgroundMusicType;

    public List<AudioClip> gameAudioClips, lobbyAudioClips;
    public float volumeChangeSpeed = 3f;

    private Coroutine coroutine;
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private float timer;

    public ClientManagerBackgroundMusic(ClientManager clientManager)
    {
        this.clientManager = clientManager;
        gameAudioClips = clientManager.clientManagerSettings.gameAudioClips;
        lobbyAudioClips = clientManager.clientManagerSettings.lobbyAudioClips;
    }

    public void OnStart()
    {
        audioSource = clientManager.transform.GetChild(0).GetComponent<AudioSource>();
        coroutine = clientManager.StartCoroutine(AdjustVolumeSmoothly());
    }

    public void ChangeBackgroundMusicType(BackgroundMusicType backgroundMusicType)
    {
        this.backgroundMusicType = backgroundMusicType;
        clientManager.StopCoroutine(coroutine);
        clientManager.StartCoroutine(AdjustVolumeSmoothly());
    }

    IEnumerator AdjustVolumeSmoothly()
    {
        audioSource.clip = (backgroundMusicType == BackgroundMusicType.GameAudio ? gameAudioClips : lobbyAudioClips)[currentClipIndex = (currentClipIndex + 1) % (backgroundMusicType == BackgroundMusicType.GameAudio ? gameAudioClips : lobbyAudioClips).Count];
        audioSource.Play();
        timer = 0f;
        while (timer < volumeChangeSpeed)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1f, timer / volumeChangeSpeed);
            yield return null;
        }
        audioSource.volume = 1f;
        yield return new WaitForSeconds(audioSource.clip.length - 2 * volumeChangeSpeed);
        timer = 0f;
        while (timer < volumeChangeSpeed)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, timer / volumeChangeSpeed);
            yield return null;
        }
        audioSource.volume = 0f;
        coroutine = clientManager.StartCoroutine(AdjustVolumeSmoothly());
    }
}
