using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIdleState : SkillState
{
    public SkillIdleState(Skill skill, SkillStateMachine skillStateMachine) : base(skill, skillStateMachine) { }

    public override SkillState HandleState()
    {
        return skillStateMachine.skillHandleState;
    }
}
