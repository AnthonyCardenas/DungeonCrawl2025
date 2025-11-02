using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start(){}

    // // Update is called once per frame
    // void Update() { }
    public static HealthBar Create(Vector3 pos, Vector3 size)
    {
        // Main Health Bar
        GameObject healthBarGameObject = new GameObject("HealthBar");
        healthBarGameObject.transform.position = pos;

        // Background
        GameObject backgroundGameObject = new GameObject("Background", typeof(SpriteRenderer));
        backgroundGameObject.transform.SetParent(healthBarGameObject.transform);
        backgroundGameObject.transform.localPosition = Vector3.zero;
        backgroundGameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        // backgroundGameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0f, 0f), 100f); 
        backgroundGameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;

        // Bar
        GameObject barGameObject = new GameObject("Bar");
        barGameObject.transform.SetParent(healthBarGameObject.transform);
        barGameObject.transform.localPosition = new Vector3(- size.x / 2f, 0f);

        // Sprite
        GameObject barSpriteGameObject = new GameObject("BarSprite", typeof(SpriteRenderer));
        barSpriteGameObject.transform.SetParent(barGameObject.transform);
        barSpriteGameObject.transform.localPosition = new Vector3(size.x / 2f, 0f); 
        barSpriteGameObject.transform.localScale = size;
        barSpriteGameObject.GetComponent<SpriteRenderer>().color = Color.green;
        // barSpriteGameObject.GetComponent<SpriteRenderer>().sprite;
        backgroundGameObject.GetComponent<SpriteRenderer>().sortingOrder = 110;

        HealthBar healthBar = healthBarGameObject.AddComponent<HealthBar>();

        Debug.Log("Created HealthBar");

        return healthBar;
    }

    private Transform bar;

    private void Awake()
    {
        bar = transform.Find("Bar");

    }
    
     public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }

}
