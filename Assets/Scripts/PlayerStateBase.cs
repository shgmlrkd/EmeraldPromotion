using UnityEngine;

public class PlayerStateBase : MonoBehaviour
{
    protected CharacterController characterController;
    protected Transform refTransform;
    protected Animator refAnimator;

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

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
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
