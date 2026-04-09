using UnityEngine;

public class ItemAssetsA : MonoBehaviour
{
    public static ItemAssetsA Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public Transform pfItemObject;

    public Sprite eggSprite;
    public Sprite petSprite;
    public Sprite keySprite;
    // public Sprite weaponSprite;
    // public Sprite armorSprite;

}
