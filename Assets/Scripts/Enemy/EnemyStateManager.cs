using UnityEngine;

public class EnemyStateManager : CharacterStateManager
{
    [SerializeField]
    private EnemyData data;
    public EnemyData Data => data;

    private EnemyStatus enemyStatus;
    public EnemyStatus EnemyStatus => enemyStatus;

    protected override void Awake()
    {
        base.Awake();
        enemyStatus = status as EnemyStatus;
    }


}