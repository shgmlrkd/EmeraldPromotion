using UnityEngine;

public class PlayerStateBase : MonoBehaviour
{
    protected CharacterController characterController;
    protected Transform refTransform;
    protected Animator refAnimator;

    protected PlayerAnimationController playerAnimationController;

    protected PlayerStateManager stateManager;

    protected virtual void OnEnable()
    {
        if (refTransform == null)
        {
            refTransform = transform;
        }

        if (refAnimator == null)
        {
            refAnimator = GetComponent<Animator>();
        }

        if (stateManager == null)
        {
            stateManager = GetComponent<PlayerStateManager>();
        }

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        if(playerAnimationController == null)
        {
            playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        }
    }

    protected virtual void Update()
    {
        characterController.Move(Physics.gravity * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        characterController.Move(refAnimator.deltaPosition);
    }
}
