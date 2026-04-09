using UnityEngine;

public class ItemSpawnerA : MonoBehaviour
{
    public ItemInfoA item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemObjectA.SpawnItemObjectA(transform.position, item);
        Destroy(gameObject);
    }
}
