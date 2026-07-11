using UnityEngine;

public enum ItemType
{
    HpPotion,
    StaminaPotion
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/Potion Data")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private ItemType type;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private float healAmount;

    [SerializeField]
    private int itemKey;

    public Sprite Sprite => sprite; 
    public ItemType Type => type;
    public string ItemName => itemName;
    public float HealAmount => healAmount;
    public int ItemKey => itemKey;
}
