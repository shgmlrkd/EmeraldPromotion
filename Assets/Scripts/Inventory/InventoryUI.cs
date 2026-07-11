using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    private bool inventoryToggle = false;
    public bool isInventoryOpen => inventoryToggle;

    private void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            inventoryToggle = !inventoryToggle;
            inventoryPanel.gameObject.SetActive(inventoryToggle);
        }
    }
}