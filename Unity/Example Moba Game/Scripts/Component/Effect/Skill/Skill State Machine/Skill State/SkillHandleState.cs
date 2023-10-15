using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHandleState : SkillState
{
    public SkillHandleState(Skill skill, SkillStateMachine skillStateMachine) : base(skill, skillStateMachine) { }

    public override SkillState HandleState()
    {
        
        return this;
    }
}
