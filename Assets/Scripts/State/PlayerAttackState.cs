using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    [SerializeField]
    private Transform attackPoint;

    [SerializeField] 
    private LayerMask enemyLayer;

    private void OnAttackHit()
    {
        Collider[] hits = Physics.OverlapSphere(
            attackPoint.position,
            stateManager.Data.attackRange,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(stateManager.Data.attackPower);
            }
        }
    }

    private void OnAttackStart()
    {
        stateManager.PlayerStatus.UseStamina(stateManager.Data.staminaAttackConsumeAmount);
    }

    private void ComboCheck()
    {
        animationController.SetAnimatorAttackCombo(stateManager.NextAttackCombo);
    }

    private void AttackEnd()
    {
        stateManager.SetState(PlayerStateManager.State.Idle);
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null || stateManager == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, stateManager.Data.attackRange);
    }
}