using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public enum State
    {
        None = -1,
        Idle,
        Walk,
        Length
    }

    [SerializeField]
    private State state = State.None;

    [SerializeField]
    private PlayerStateBase[] playerStates;

    private PlayerAnimationController animationController;

    private void Awake()
    {
        if (animationController == null)
        {
            animationController = GetComponentInChildren<PlayerAnimationController>();
        }
    }

    private void OnEnable()
    {
        SetState(State.Idle);
    }

    private void Update()
    {
        if (InputManager.Movement != Vector2.zero)
        {
            SetState(State.Walk);
        }
        else
        {
            SetState(State.Idle);
        }

        animationController.SetAnimationState(state);
    }

    private void SetState(State newState)
    {
        if (state == newState) return;

        if (state != State.None)
        {
            playerStates[(int)state].enabled = false;
        }

        state = newState;

        playerStates[(int)state].enabled = true;
    }
}
