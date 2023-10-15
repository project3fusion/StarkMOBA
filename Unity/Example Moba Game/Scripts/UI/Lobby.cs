using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject welcomeGameObject, characterSelectGameObject, loadingGameObject;
    public GameObject backgroundCanvasGameObject;

    public static int championID;

    public void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(backgroundCanvasGameObject);
    }

    public void SelectRangedCharacter()
    {
        championID = 2;
        EnterLoading();
        SceneManager.LoadSceneAsync(1);
    }

    public void SelectMeleeCharacter()
    {
        championID = 1;
        EnterLoading();
        SceneManager.LoadSceneAsync(1);
    }

    public void EnterCharacterSelection()
    {
        welcomeGameObject.SetActive(false);
        characterSelectGameObject.SetActive(true);
    }

    public void EnterLoading()
    {
        characterSelectGameObject.SetActive(false);
        loadingGameObject.SetActive(true);
    }

    public void DisableLobbyUI()
    {
        Destroy(backgroundCanvasGameObject);
        Destroy(gameObject);
    }
}
