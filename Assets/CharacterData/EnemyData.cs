using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Character/EnemyData")]
public class EnemyData : CharacterData
{
    public float detectionRange;
    public float chaseRange;
}