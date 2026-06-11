using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private float damageInterval = 2.0f;
    private float timer = 0.0f;

    [SerializeField]
    private float damageAmount = 15.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerStatus player))
        {
            if (player.IsDead) return;

            timer += Time.deltaTime;

            if (timer > damageInterval)
            {
                timer = 0.0f;
                player.TakeDamage(damageAmount);
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
