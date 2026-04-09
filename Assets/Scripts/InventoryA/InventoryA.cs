using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class InventoryA
{
    public event EventHandler OnItemListChanged;
    private List<ItemInfoA> itemList;

    public InventoryA()
    {
        itemList = new List<ItemInfoA>();

        AddItem(new ItemInfoA{ itemType = ItemInfoA.ItemType.Egg, amount = 1});
        AddItem(new ItemInfoA{ itemType = ItemInfoA.ItemType.Pet, amount = 1});
        // Debug.Log(itemList.Count);
    }

    public void AddItem(ItemInfoA item)
    {
        itemList.Add(item);
        // Debug.Log($"Num items in the inventory {itemList.Count}");
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<ItemInfoA> GetItemList()
    {
        return itemList;
    }
}
