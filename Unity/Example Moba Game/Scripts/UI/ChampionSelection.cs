using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionSelection : MonoBehaviour
{
    public int championID;

    public void SelectChampion()
    {
        Player.Owner.PlayerSelectChampionServerRpc(championID);
        transform.parent.gameObject.SetActive(false);
    }
}
