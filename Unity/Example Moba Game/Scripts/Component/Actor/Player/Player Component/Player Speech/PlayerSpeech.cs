using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeech
{
    private Player player;

    private int lastIndex = -1;
    private WaitForSeconds interval;
    private AudioSource playerSpeechAudioSource;
    private Coroutine randomSpeechCoroutine;

    public PlayerSpeech(Player player) 
    {
        this.player = player;
        playerSpeechAudioSource = player.playerSettings.playerSpeechAudioSource;
    }

    public void OnStart()
    {
        interval = new WaitForSeconds(10f);
        randomSpeechCoroutine = player.StartCoroutine(PlayRandomSpeech());
    }

    public void DeathSpeech()
    {
        player.StopCoroutine(randomSpeechCoroutine);
        playerSpeechAudioSource.clip = player.champion.deathSpeechAudioClip;
        playerSpeechAudioSource.Play();
    }

    private IEnumerator PlayRandomSpeech()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return interval;
            int randomIndex;
            randomIndex = Random.Range(0, player.champion.speechAudioClips.Count);
            if (player.champion.speechAudioClips.Count > 1) randomIndex = (randomIndex == lastIndex) ? ((randomIndex + 1 >= player.champion.speechAudioClips.Count) ? randomIndex - 1 : randomIndex + 1) : randomIndex;
            lastIndex = randomIndex;
            playerSpeechAudioSource.clip = player.champion.speechAudioClips[randomIndex];
            playerSpeechAudioSource.Play();
        }
    }
}
