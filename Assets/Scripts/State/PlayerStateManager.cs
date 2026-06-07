using UnityEngine;
using UnityEngine.Events;

public class PlayerStateManager : MonoBehaviour
{
    public enum State
    {
        None = -1,
        Idle,
        Walk,
        Attack,
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
    private State state = State.None;

    public Combat NextAttackCombo { get; private set; } = Combat.None;

    [SerializeField]
    private UnityEvent<State> OnStateChanged;

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
        if (InputManager.IsAttack)
        {
            if (state != State.Attack)
            {
                NextAttackCombo = Combat.AttackCombo1;

                SetState(State.Attack);
                return;
            }

            AnimatorStateInfo curStateInfo = animationController.GetCurrentStateInfo();

            if (curStateInfo.IsName("AttackCombo1"))
            { 
                NextAttackCombo = Combat.AttackCombo2;
            }
            else if (curStateInfo.IsName("AttackCombo2"))
            { 
                NextAttackCombo = Combat.AttackCombo3;
            }
            else
            { 
                NextAttackCombo = Combat.None;
            }
        }

        if (state == State.Attack) return;

        if (InputManager.Movement != Vector2.zero)
        {
            SetState(State.Walk);
        }
        else
        {
            SetState(State.Idle);
        }
    }

    public void SetState(State newState)
    {
        if (state == newState) return;

        if (state != State.None)
        {
            playerStates[(int)state].enabled = false;
        }

        state = newState;

        playerStates[(int)state].enabled = true;

        OnStateChanged?.Invoke(state);
    }
}
