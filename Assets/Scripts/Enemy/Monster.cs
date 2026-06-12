using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private EnemyData data;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private float moveSpeed = 1.5f;

    [SerializeField]
    private float attackDelay = 2.5f;

    private float attackTimer;

    private Transform target;

    private Renderer renderer;

    private Coroutine hitEffectCoroutine;

    private Material defaultMaterial;
    private Material hitMaterial;

    private float hitEndTime;

    private bool isHit => Time.time < hitEndTime;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();

        defaultMaterial = renderer.materials[0];
        hitMaterial = renderer.materials[1];
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        FindPlayer();

        if (target == null)
            return;

        if(target.TryGetComponent(out PlayerStatus player))
        {
            if (player.CurHp <= 0) return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= data.attackRange)
        {
            Attack();
        }
        else if (distance <= data.chaseRange)
        {
            Chase();
        }
    }

    private void FindPlayer()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > data.chaseRange)
            {
                target = null;
                return;
            }
        }

        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            data.detectionRange,
            playerLayer);

        if (colliders.Length > 0)
        {
            target = colliders[0].transform;
        }
    }

    private void Chase()
    {
        if (isHit)
            return;

        Vector3 dir = (target.position - transform.position).normalized;

        dir.y = 0.0f;

        transform.position += dir * moveSpeed * Time.deltaTime;

        transform.forward = dir;
    }

    private void Attack()
    {
        if (isHit) return;

        transform.LookAt(
            new Vector3(
                target.position.x,
                transform.position.y,
                target.position.z));

        if (attackTimer < attackDelay)
            return;

        attackTimer = 0.0f;

        if (target.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(data.attackPower);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (data == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }

    public void StartHitEffect()
    {
        if(hitEffectCoroutine != null)
        {
            StopCoroutine(HitEffect());
            hitEffectCoroutine = null;
        }

        hitEffectCoroutine = StartCoroutine(HitEffect());
    }

    private IEnumerator HitEffect()
    {
        Material[] materials = renderer.materials;

        materials[0] = hitMaterial;
        renderer.materials = materials;

        yield return new WaitForSeconds(0.3f);

        materials[0] = defaultMaterial;
        renderer.materials = materials;

        hitEffectCoroutine = null;
    }
}