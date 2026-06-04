using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator refAnimator;

    private int stateParameterHash = Animator.StringToHash("State");

    public void SetAnimationState(PlayerStateManager.State newState)
    {
        refAnimator.SetInteger(stateParameterHash, (int)newState);
    }
}
