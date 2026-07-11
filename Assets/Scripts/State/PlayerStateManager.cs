using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : CharacterStateManager
{
    [SerializeField]
    private InventoryUI inventoryUI;

    [SerializeField]
    private PlayerData data;
    public PlayerData Data => data;

    private PlayerStatus playerStatus;

    public PlayerStatus PlayerStatus => playerStatus;
    public InventoryUI  InventoryUI => inventoryUI;

    protected override void Awake()
    {
        base.Awake();
        playerStatus = status as PlayerStatus;
    }

    private void Update()
    {
        if (status.IsDead || status.IsHit) return;

        if (inventoryUI.isInventoryOpen)
        {
            SetState(State.Idle);
            return;
        }

        if (InputManager.IsAttack && 
            playerStatus.CurStamina - data.staminaAttackConsumeAmount >= 0.0f)
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
            State moveState = 
                InputManager.IsRun && playerStatus.CurStamina > 0.0f ?
                State.Run : State.Walk;

            SetState(moveState);
        }
        else
        {
            SetState(State.Idle);
        }
    }
}
