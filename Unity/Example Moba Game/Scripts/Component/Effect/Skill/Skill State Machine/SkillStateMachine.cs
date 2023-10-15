using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStateMachine
{
    private Skill skill;

    public SkillIdleState skillIdleState;
    public SkillHandleState skillHandleState;
    public SkillState currentSkillState;

    public SkillStateMachine(Skill skill)
    {
        this.skill = skill;
        skillIdleState = new SkillIdleState(skill, this);
        skillHandleState = new SkillHandleState(skill, this);
        currentSkillState = skillIdleState;
    }

    public SkillState OnOptimizedUpdate() => currentSkillState = currentSkillState.HandleState();
}
