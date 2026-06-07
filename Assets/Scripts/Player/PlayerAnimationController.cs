using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator refAnimator;

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
}
