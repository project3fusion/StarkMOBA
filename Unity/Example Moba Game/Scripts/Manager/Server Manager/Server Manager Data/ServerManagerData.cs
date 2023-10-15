using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerManagerData
{
    public List<ServerManagerTeamData> serverManagerTeamData;

    public ServerManagerData()
    {
        serverManagerTeamData = new List<ServerManagerTeamData>
        {
            new ServerManagerTeamData(),
            new ServerManagerTeamData()
        };
    }
}