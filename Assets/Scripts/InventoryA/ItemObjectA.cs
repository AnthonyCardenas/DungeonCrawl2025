using UnityEngine;
using System.Collections;
// using UnityEngine.Experimental.Rendering.LWRP;

public class ItemObjectA : MonoBehaviour
{

    private ItemInfoA item;
    private SpriteRenderer spriteRenderer;
    // private Light2D light2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // light2D = GetComponent<Light2D>();
    }

    public static ItemObjectA SpawnItemObjectA(Vector3 position, ItemInfoA item)
    {
        Transform transform = Instantiate(ItemAssetsA.Instance.pfItemObject, position, Quaternion.identity);
    
        ItemObjectA itemObject = transform.GetComponent<ItemObjectA>();
        itemObject.SetItem(item);

        return itemObject;
    }

    public static ItemObjectA DropItem(Vector3 dropPosition, ItemInfoA item)
    {
        float randX = Random.Range(-2f, 2f);
        float randY = Random.Range(-2f, 2f);
        Vector3 randDir = new Vector3(randX, randY, 0);
        randDir.Normalize();
        ItemObjectA ItemObject = SpawnItemObjectA(dropPosition + randDir * 3f, item);
        ItemObject.GetComponent<Rigidbody2D>().AddForce(randDir * 3f, ForceMode2D.Impulse);
        return ItemObject;
    }

    public void SetItem(ItemInfoA item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        // light2D.color = item.GetColor();
    }
    public ItemInfoA GetItem()
    {
        // Debug.Log("Getting the item info from the item object.");
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
