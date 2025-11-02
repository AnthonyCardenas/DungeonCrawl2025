using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // public Texture2D spriteTexture = new Texture2D(64, 64); // width, height
    // public Texture2D st = Resources.Load<Texture2D>("Circle");
    public float pixelsPerUnit = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public static Enemy Create(Vector3 pos, Color spriteColor, Vector3 size)
    {
        Debug.Log("Started Enemy Creater");
        // Main Health Bar
        GameObject enemyGameObject = new GameObject("Enemy");
        enemyGameObject.transform.position = pos;

        // Add Sprite
        SpriteRenderer spriteRenderer = enemyGameObject.AddComponent<SpriteRenderer>();
        Debug.Log("Started Enemy Sprite Render");

        Texture2D tex = Resources.Load<Texture2D>("Circle");
        Sprite newSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.zero, 100f);
        spriteRenderer.sprite = newSprite;

        Debug.Log("Started Enemy texture");

        spriteRenderer.color = spriteColor;
        // spriteRenderer.color = Color.red;

        // //use textures to create sprite
        // if (st != null)
        // {
        //     var newSprite = Sprite.Create(st, new Rect(0f, 0f, st.width, st.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
        //     spriteRenderer.sprite = newSprite;
        // }
        // else
        // {
        //     // var texture = Resources.Load<Texture2D>("FilePath/sprite");
        //     var sprite = Sprite.Create(new Texture2D(64, 64), new Rect(0, 0, 2, 3), Vector2.zero, 100f); // texture, shape(pos, shape), position
        //     spriteRenderer.sprite = sprite;
        //     Debug.LogWarning("Sprite Texture not assigned");
        // }
        
        // spriteRenderer.sortingOrder = 10;

        // Add Rigidbody
        Rigidbody2D rb2d = enemyGameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.linearDamping = 3f;
        rb2d.mass = 1f;
        Debug.Log("Started Enemy Rigidbody");

        //Add Circle Colliders
        CircleCollider2D circleCollider2D = enemyGameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.radius = 1.5f;
        Debug.Log("Started Enemy Collider");

        Enemy enemy = enemyGameObject.AddComponent<Enemy>();

        Debug.Log("Created Enemy");

        return enemy;
    }

    public void SetSize(float sizeNormalized)
    {
        // bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color)
    {
        // bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
