using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    [SerializeField]
    private Transform attackPoint;

    [SerializeField] 
    private LayerMask enemyLayer;

    [SerializeField] 
    private float attackRadius = 0.4f;

    private void OnAttackHit()
    {
        Collider[] hits = Physics.OverlapSphere(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                print("때림");
            }
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                print("때림");
                damageable.TakeDamage(10);
            }
        }
    }

    private void ComboCheck()
    {
        playerAnimationController.SetAnimatorAttackCombo(stateManager.NextAttackCombo);
    }

    private void AttackEnd()
    {
        stateManager.SetState(PlayerStateManager.State.Idle);
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}