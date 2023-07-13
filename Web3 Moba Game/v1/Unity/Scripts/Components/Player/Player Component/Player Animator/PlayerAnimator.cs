using UnityEngine;

[System.Serializable]
public class PlayerAnimator
{
    private Player player;
    private Animator animator;

    public PlayerAnimator(Player player) => this.player = player;

    public void OnStart() => animator = player.transform.GetComponent<Animator>();

    public void ResetAnimator()
    {
        animator.SetBool("Run", false);
        animator.Rebind();
    }

    public void PlayRunAnimation(bool isRunning) => animator.SetBool("Run", isRunning);

    public void PlayAttackAnimation(string animationName) 
    {
        ResetAnimator();
        animator.Play(animationName); 
    }
}