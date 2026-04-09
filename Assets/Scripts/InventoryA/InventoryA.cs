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
        AddItem(new ItemInfoA{ itemType = ItemInfoA.ItemType.Key, amount = 2});
        // Debug.Log(itemList.Count);
    }
    
    public List<ItemInfoA> GetItemList()
    {
        return itemList;
    }

    public void AddItem(ItemInfoA item)
    {
        if(item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(ItemInfoA inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                    continue;
                }
            }
            if(!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        } else
        {
            itemList.Add(item);
        }
        
        // Debug.Log($"Num items in the inventory {itemList.Count}");
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(ItemInfoA item)
    {
        if(item.IsStackable())
        {
            ItemInfoA itemInInventory = null;
            foreach(ItemInfoA inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                    continue;
                }
            }
            if(itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        } else
        {
            itemList.Remove(item);
        }
        
        // Debug.Log($"Num items in the inventory {itemList.Count}");
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
}
