using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator refAnimator;

    private int hitStringToHash = Animator.StringToHash("Hit");
    private int deadStringToHash = Animator.StringToHash("Dead");
    private int stateStringToHash = Animator.StringToHash("State");
    private int attackComboStringToHash = Animator.StringToHash("AttackCombo");

    public AnimatorStateInfo GetCurrentStateInfo()
    {
        return refAnimator.GetCurrentAnimatorStateInfo(0);
    }

    public void SetAnimatorState(PlayerStateManager.State newState)
    {
        refAnimator.SetInteger(stateStringToHash, (int)newState);
    }

    public void SetAnimatorAttackCombo(PlayerStateManager.Combat newState)
    {
        refAnimator.SetInteger(attackComboStringToHash, (int)newState);
    }

    public void SetAnimatorHitTrigger()
    {
        refAnimator.SetTrigger(hitStringToHash);
    }

    public void SetAnimatorDeadTrigger()
    {
        refAnimator.SetTrigger(deadStringToHash);
    }
}