using UnityEngine;

public class HealItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth player))
        {
            player.Heal(30);
            Destroy(gameObject);
        }
    }
}