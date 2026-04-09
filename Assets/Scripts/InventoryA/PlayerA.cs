using UnityEngine;

public class PlayerA : MonoBehaviour
{
    // private float Speed = 10f;

    // private MaterialTintColor materialTintColor;
    [SerializeField] private UI_InventoryA uiInventory;
    // private Player_Base playerBase;
    // private State state;
    private InventoryA inventory;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Instance = this;
        // playerBase = gameObject.GetComponent<Player_Base>();
        // SetStateNormal();

        inventory = new InventoryA();
        uiInventory.SetInventory(inventory);

        ItemObjectA.SpawnItemObjectA(new Vector3(5, 0), new ItemInfoA{ itemType=ItemInfoA.ItemType.Pet, amount = 1});
        ItemObjectA.SpawnItemObjectA(new Vector3(0, 3), new ItemInfoA{ itemType=ItemInfoA.ItemType.Egg, amount = 1});
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Item")
        {
            Debug.Log("Collided with object that was not an item.");
            return;
        }
        Debug.Log("Collided with object that has the item tag.");
        ItemObjectA itemObject = collider.GetComponent<ItemObjectA>();
        if(itemObject != null)
        {
            //  touching an item
            inventory.AddItem(itemObject.GetItem());
            itemObject.DestroySelf();
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
    // }
}
