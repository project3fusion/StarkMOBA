using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStateMachineChecker : MonoBehaviour
{
    private Skill skill;
    private SkillStateMachine skillStateMachine;

    public MultiRangeChecker multiRangeChecker;

    public SkillStateMachineChecker(Skill skill, SkillStateMachine skillStateMachine)
    {
        this.skill = skill;
        this.skillStateMachine = skillStateMachine;
    }
}
