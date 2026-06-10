using UnityEngine;

public class CharacterStateBase : MonoBehaviour
{
    protected CharacterController characterController;
    protected Transform refTransform;
    protected Animator refAnimator;

    protected AnimationController animationController;

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

        if (animationController == null)
        {
            animationController = GetComponentInChildren<AnimationController>();
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