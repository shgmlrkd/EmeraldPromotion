using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Character/PlayerData")]
public class PlayerData : CharacterData
{
    public float maxStamina;
    public float staminaAttackConsumeAmount;
    public float staminaRunConsumeAmount;
    public float staminaRecoveryAmount;
    public float staminaRecoveryDelay;
}