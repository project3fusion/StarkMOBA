using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimator
{
    private Minion minion;
    private Animator animator;

    public MinionAnimator(Minion minion) => this.minion = minion;

    public void OnStart() => animator = minion.transform.GetComponent<Animator>();

    public void OnUpdate()
    {
        if (minion.minionData.Value.minionAnimationData.minionAnimationState == MinionAnimationData.MinionAnimationState.Idle) PlayRunAnimation(false);
        else PlayRunAnimation(true);
    }

    public void ResetAnimator()
    {
        animator.SetBool("Run", false);
        animator.Rebind();
    }

    public void PlayRunAnimation(bool isRunning) => animator.SetBool("Run", isRunning);

    public void PlayAnimation(string animationName) => animator.Play(animationName);

    public void PlayAttackAnimation(string animationName)
    {
        ResetAnimator();
        animator.Play(animationName);
    }
}
