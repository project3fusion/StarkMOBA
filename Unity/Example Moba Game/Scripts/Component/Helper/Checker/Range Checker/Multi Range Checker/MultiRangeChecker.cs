using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRangeChecker : RangeChecker
{
    public List<Actor> targets;

    public List<List<Actor>> actors;

    private float checkerRangeSqr;
    private int enemyTeam;

    public MultiRangeChecker(Component component, float range) : base(component)
    {
        checkerRangeSqr = range * range;
        enemyTeam = component.team == Component.Team.Blue ? 1 : 0;
        targets = new List<Actor>();
        actors = new List<List<Actor>>
        {
            ServerManager.Instance.serverManagerData.serverManagerTeamData[enemyTeam].teamTowers,
            ServerManager.Instance.serverManagerData.serverManagerTeamData[enemyTeam].teamMinions,
            ServerManager.Instance.serverManagerData.serverManagerTeamData[enemyTeam].teamPlayers
        };
    }

    public bool CheckEnemyTargets()
    {
        targets.Clear();
        foreach (List<Actor> selectedTargets in actors) if (CheckTargets(selectedTargets)) return true;
        return false;
    }

    public bool CheckTargets(List<Actor> selectedTargets)
    {
        foreach (Actor selectedActor in selectedTargets)
            if ((selectedActor.transform.position - component.transform.position).sqrMagnitude <= checkerRangeSqr)
                SetTarget(selectedActor);
        if (targets.Count > 0) return true;
        else return false;
    }

    public void SetTarget(Actor actor) => targets.Add(actor);
}
