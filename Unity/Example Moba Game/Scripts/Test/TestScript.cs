using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void Start()
    {
        StartHost();
        StartCoroutine(SelectCharacter());
    }

    public IEnumerator SelectCharacter()
    {
        yield return new WaitForSeconds(1);
        if (Player.Owner != null)
        {
            Player.Owner.PlayerSelectChampionServerRpc(Lobby.championID);
            GameObject.Find("Lobby Canvas").GetComponent<Lobby>().DisableLobbyUI();
        }
        else StartCoroutine(SelectCharacter());
    }
    
    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public static List<string> playernames = new List<string>
    {
        "MagicWizard",
        "FireWarrior",
        "MasterArcher",
        "EpicKnight"
    };

    public static string GenerateRandomPlayername() => playernames[Random.Range(0, 4)];
}
