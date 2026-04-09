using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_InventoryA : MonoBehaviour
{
    private InventoryA inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private int numCol = 4;
    private float xOffset = 75f;
    private float yOffset = -50f;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(InventoryA inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    // GUI image placement for inventory
    private void RefreshInventoryItems()
    {
        // destroy all items to prevent duplicate copies in inventory
        foreach(Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 120f;
        foreach(ItemInfoA item in inventory.GetItemList())
        {
            // place new slot
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + xOffset, y * itemSlotCellSize + yOffset);
            // Change slot image
            Image image = itemSlotRectTransform.Find("SlotImage").GetComponent<Image>();
            image.sprite = item.GetSprite();
            // Advance slot position
            x++;
            if(x > numCol)
            {
                x = 0;
                y++;
            }
        }
    }
}
