using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRangeChecker : RangeChecker
{
    public Actor target;

    public List<List<Actor>> actors;

    private float checkerRangeSqr, tempDistance, targetDistance;
    private int enemyTeam;

    public SingleRangeChecker(Component component, float range) : base(component)
    {
        checkerRangeSqr = range * range;
        enemyTeam = component.team == Component.Team.Blue ? 1 : 0;
        actors = new List<List<Actor>>
        {
            ServerManager.Instance.serverManagerData.serverManagerTeamData[enemyTeam].teamTowers,
            ServerManager.Instance.serverManagerData.serverManagerTeamData[enemyTeam].teamMinions,
            ServerManager.Instance.serverManagerData.serverManagerTeamData[enemyTeam].teamPlayers
        };
    }

    public bool CheckEnemyTargets()
    {
        if (CheckExistingTargets()) return true;
        foreach (List<Actor> selectedTargets in actors) if (CheckTargets(selectedTargets)) return true;
        return false;
    }

    public bool CheckExistingTargets()
    {
        if (target != null && !target.isDead.Value && (target.transform.position - component.transform.position).sqrMagnitude <= checkerRangeSqr) return true;
        target = null;
        return false;
    }

    public bool CheckTargets(List<Actor> selectedTargets)
    {
        foreach (Actor selectedActor in selectedTargets)
            if ((tempDistance = (selectedActor.transform.position - component.transform.position).sqrMagnitude) <= checkerRangeSqr)
                if (target == null) SetTarget(selectedActor, tempDistance);
                else if (tempDistance <= targetDistance) SetTarget(selectedActor, tempDistance);
        if (target != null) return true;
        else return false;
    }

    public void SetTarget(Actor actor, float distance)
    {
        target = actor;
        targetDistance = distance;
    }
}
