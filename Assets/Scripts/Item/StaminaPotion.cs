using UnityEngine;

public class StaminaPotion : MonoBehaviour
{
    [SerializeField]
    private ItemData staminaPotionData;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Inventory inventory))
        {
            inventory.CollectedItem(staminaPotionData);

            Destroy(gameObject);
        }
    }
}
