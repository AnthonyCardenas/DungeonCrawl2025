using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class UI_InventoryA : MonoBehaviour
{
    private InventoryA inventory;
    // [SerializeField] 
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private PlayerA player;
    // [SerializeField] private Transform backupItemSlotContainer;
    // [SerializeField] private Transform backupItemSlotTemplate;
    private int numCol = 4;
    private float xOffset = 75f;
    private float yOffset = -50f;

    // Awake because it needs to start before playerA script
    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        if(itemSlotContainer == null)
        {
            // itemSlotContainer = backupItemSlotContainer;
            Debug.Log("itemSlotContainer not found on awake");
        }

        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        if(itemSlotTemplate == null )
        {
            // itemSlotTemplate = backupItemSlotContainer;
            Debug.Log("itemSlotTemplate not found on awake");
        }
    }

    public void SetPlayer(PlayerA player)
    {
        this.player = player;
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
        // check inventory ui container and slots
        if(itemSlotContainer == null)
        {
            Debug.Log("itemSlotContainer not found on refresh");
            return;
        }
        if(itemSlotTemplate == null)
        {
            Debug.Log("itemSlotTemplate not found  on refresh");
            return;
        }

        // destroy all items to prevent duplicate copies in inventory
        foreach(Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        // Place an image in each slot for items in the inventory
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 120f;
        foreach(ItemInfoA item in inventory.GetItemList())
        {
            // place new slot
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            // Activate button ui (Need Code Monkey Utils)
            // itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            // {
            //     // Use item
            // };
            // itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            // {
            //     // Drop item
            //     inventory.RemoveItem(item);
            //     ItemObjectA.DropItem(player.GetPosition(), item);
            // };
            // Change slot image
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + xOffset, y * itemSlotCellSize + yOffset);
            Image image = itemSlotRectTransform.Find("SlotImage").GetComponent<Image>();
            image.sprite = item.GetSprite();
            // Change slot amount text
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            } else
            {
                uiText.SetText("");
            }
            
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
