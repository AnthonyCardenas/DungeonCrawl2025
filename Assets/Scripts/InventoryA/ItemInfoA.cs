using System;
using UnityEngine;

[Serializable]
public class ItemInfoA
{
   public enum ItemType
    {
        // Weapon,
        // Armor,
        Egg,
        Pet,
        Key,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            // case ItemType.Weapon:
            //     return ItemAssetsA.Instance.weaponSprite;
            case ItemType.Egg:
                return ItemAssetsA.Instance.eggSprite;
            case ItemType.Pet:
                return ItemAssetsA.Instance.petSprite;
            case ItemType.Key:
                return ItemAssetsA.Instance.keySprite;
            // case ItemType.Armor:
            //     return ItemAssetsA.Instance.armorSprite;
        }
    }

    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Egg:
                return new Color(0, 0, 0);
            case ItemType.Pet:
                return new Color(0, 0, 0);
            case ItemType.Key:
                return new Color(0, 0, 0);

        }
    }

    public bool IsStackable()
    {
        switch(itemType)
        {
            default:
            case ItemType.Egg:
            case ItemType.Pet:
                return false;
            case ItemType.Key:
                return true;
        }
    }
}
