public class EnemyStatus : CharacterStatus, IDamageable
{
    private EnemyStateManager stateManager;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
    }

    protected override void Start()
    {
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
            curHp -= damage;
            isHit = true;
        }

        UpdateHpUI(curHp);
    }
}