using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public ItemData Data;
    public int Index = -1;
    public int Count = 0;

    public ItemStack(ItemData data)
    {
        Data = data;
        Count = 1;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus status;

    private List<ItemStack> itemList = new();
    private Dictionary<int, ItemStack> itemMap = new();

    public event Action<int, int, Sprite> OnUpdateInventory;

    public void UseItem(int index)
    {
        if (index < 0 || index >= itemList.Count) return;

        ItemStack item = itemList[index];

        if (item == null) return;

        item.Count--;

        ApplyItemEffect(item.Data);

        if (item.Count <= 0)
        {
            RemoveItem(item);
        }
        else
        {
            // 개수만 줄어든 경우는 해당 슬롯만 갱신
            OnUpdateInventory?.Invoke(item.Index, item.Count, item.Data.Sprite);
        }
    }

    private void ApplyItemEffect(ItemData data)
    {
        switch (data.Type)
        {
            case ItemType.HpPotion:
                status.Heal(data.HealAmount);
                break;
            case ItemType.StaminaPotion:
                status.StaminaHeal(data.HealAmount);
                break;
        }
    }

    private void RemoveItem(ItemStack item)
    {
        int removedIndex = item.Index;

        itemMap.Remove(item.Data.ItemKey);
        itemList[removedIndex] = null;

        OnUpdateInventory?.Invoke(item.Index, item.Count, null);
    }

    public void CollectedItem(ItemData data)
    {
        if (itemMap.TryGetValue(data.ItemKey, out ItemStack existing))
        {
            existing.Count++;
            OnUpdateInventory?.Invoke(existing.Index, existing.Count, data.Sprite);
            return;
        }

        ItemStack newItem = new ItemStack(data);

        // 비어있는 슬롯(null)을 먼저 찾아서 재사용
        int emptyIndex = itemList.FindIndex(x => x == null);
        if (emptyIndex != -1)
        {
            newItem.Index = emptyIndex;
            itemList[emptyIndex] = newItem;
        }
        else
        {
            newItem.Index = itemList.Count;
            itemList.Add(newItem);
        }

        itemMap.Add(data.ItemKey, newItem);
        OnUpdateInventory?.Invoke(newItem.Index, newItem.Count, data.Sprite);
    }
}