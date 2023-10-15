using UnityEngine;

[System.Serializable]
public class PlayerAnimator
{
    private Player player;
    private Animator animator;

    public PlayerAnimator(Player player) => this.player = player;

    public void OnStart() => animator = player.transform.GetComponent<Animator>();

    public void OnUpdate() => PlayClientAnimations();

    public void PlayRunAnimation(bool isRunning) => animator.SetBool("Run", isRunning);

    public void PlayAnimation(string animationName) => animator.Play(animationName);

    public void PlayClientAnimations()
    {
        if (player.playerData.Value.playerAnimationData.playerAnimationState == PlayerAnimationData.PlayerAnimationState.Idle) player.playerAnimator.PlayRunAnimation(false);
        else if (player.playerData.Value.playerAnimationData.playerAnimationState == PlayerAnimationData.PlayerAnimationState.Run) player.playerAnimator.PlayRunAnimation(true);
    }
}