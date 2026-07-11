using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField]
    private ItemData healPotionData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Inventory inventory))
        {
            inventory.CollectedItem(healPotionData);

            Destroy(gameObject);
        }
    }

}
