using UnityEngine;

public class HealZone : MonoBehaviour
{
    private float healInterval = 2.0f;
    private float timer = 0.0f;

    [SerializeField]
    private float healAmount = 10.0f;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out PlayerStatus player))
        {
            timer += Time.deltaTime;

            if (timer > healInterval)
            {
                timer = 0.0f;
                player.Heal(healAmount);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerStatus player))
        {
            timer = 0.0f;
        }
    }
}