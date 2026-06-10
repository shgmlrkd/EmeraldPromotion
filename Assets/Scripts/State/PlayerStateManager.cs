using UnityEngine;

public class PlayerStateManager : CharacterStateManager
{
    private PlayerHealth playerHp;

    protected override void Awake()
    {
        base.Awake();
        playerHp = GetComponent<PlayerHealth>();     
    }

    private void Update()
    {
        if (playerHp.IsDead || playerHp.IsHit) return;

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
            SetState(InputManager.IsRun ? State.Run : State.Walk);
        }
        else
        {
            SetState(State.Idle);
        }
    }
}
