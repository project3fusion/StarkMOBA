using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagerChampionSpeech
{
    private ClientManager clientManager;

    public AudioClip[] audioClips;
    public AudioSource audioSource;
    private int lastIndex = -1;
    private WaitForSeconds interval;

    public ClientManagerChampionSpeech(ClientManager clientManager) => this.clientManager = clientManager;

    private void Start()
    {
        interval = new WaitForSeconds(Random.Range(7f, 10f));
        clientManager.StartCoroutine(PlayRandomAudio());
    }

    private IEnumerator PlayRandomAudio()
    {
        while (true)
        {
            yield return interval;
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, audioClips.Length);
            } while (randomIndex == lastIndex);
            lastIndex = randomIndex;
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
            interval = new WaitForSeconds(Random.Range(7f, 10f));
        }
    }
}
