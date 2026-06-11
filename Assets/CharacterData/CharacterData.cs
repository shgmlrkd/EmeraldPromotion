using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float maxHp;
    public float attackPower;
    public float attackRange;
}