using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Effect
{
    //There will be a wait time
    //Many features of a skill will be implemented here
    //dynamic system is required to be implemented!

    public SkillStateMachine skillStateMachine;

    private void Start() => skillStateMachine = new SkillStateMachine(this);

    public void OnOptimizedUpdate() => skillStateMachine.OnOptimizedUpdate();
}
