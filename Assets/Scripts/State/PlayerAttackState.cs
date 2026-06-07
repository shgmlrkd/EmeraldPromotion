using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    private void ComboCheck()
    {
        playerAnimationController.SetAnimatorAttackCombo(stateManager.NextAttackCombo);
    }

    private void AttackEnd()
    {
        stateManager.SetState(PlayerStateManager.State.Idle);
    }
}