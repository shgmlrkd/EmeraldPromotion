using UnityEngine;

public class EnemyStatus : CharacterStatus, IDamageable
{
    private Monster monster;
    //private EnemyStateManager stateManager;
    [SerializeField]
    private EnemyData data;
    public EnemyData Data => data;
    private void Awake()
    {
        //stateManager = GetComponent<EnemyStateManager>();
        monster = GetComponent<Monster>();
    }

    protected override void Start()
    {
        SetHp(data.maxHp);
        base.Start(); 
    }

    
    public void TakeDamage(float damage)
    {
        if (curHp - damage <= 0)
        {
            curHp = 0.0f;
            isDead = true;
        }
        else
        {
            monster.StartHitEffect();

            curHp -= damage;
            isHit = true;
        }

        UpdateHpUI(curHp);

        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}