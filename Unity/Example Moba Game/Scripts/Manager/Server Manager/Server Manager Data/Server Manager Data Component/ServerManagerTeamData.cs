using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerManagerTeamData
{
    public List<Actor> teamMinions, teamPlayers, teamTowers;

    public ServerManagerTeamData()
    {
        teamMinions = new List<Actor>();
        teamPlayers = new List<Actor>();
        teamTowers = new List<Actor>();
    }
}
