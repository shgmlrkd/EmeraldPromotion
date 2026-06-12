using UnityEngine;
using UnityEngine.Events;

public class CharacterStateManager : MonoBehaviour
{
    public enum State
    {
        None = -1,
        Idle,
        Walk,
        Run,
        Attack,
        Hit,
        Dead,
        Length
    }

    public enum Combat
    {
        None = -1,
        AttackCombo1,
        AttackCombo2,
        AttackCombo3,
        Length
    }

    [SerializeField]
    protected State state = State.None;
    public State CurState => state;

    public Combat NextAttackCombo { get; protected set; } = Combat.None;

    [SerializeField]
    protected UnityEvent<State> OnStateChanged;

    [SerializeField]
    protected CharacterStateBase[] characterStates;

    protected AnimationController animationController;

    protected CharacterStatus status;

    protected virtual void Awake()
    {
        status = GetComponent<CharacterStatus>();
        animationController = GetComponentInChildren<AnimationController>();
    }

    private void OnEnable()
    {
        SetState(State.Idle);
    }

    public void SetState(State newState)
    {
        if (state == newState) return;

        if (state != State.None)
        {
            characterStates[(int)state].enabled = false;
        }

        state = newState;

        characterStates[(int)state].enabled = true;

        OnStateChanged?.Invoke(state);
    }

    public void SetIdleState()
    {
        SetState(State.Idle);
    }

    public void SetHitState()
    {
        SetState(State.Hit);
    }

    public void SetDeadState()
    {
        SetState(State.Dead);
    }
}