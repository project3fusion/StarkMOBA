using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillState
{
    public Skill skill;
    public SkillStateMachine skillStateMachine;

    public SkillState(Skill skill, SkillStateMachine skillStateMachine)
    {
        this.skill = skill;
        this.skillStateMachine = skillStateMachine;
    }

    public abstract SkillState HandleState();
}
