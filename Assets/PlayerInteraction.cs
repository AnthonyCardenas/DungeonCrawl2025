using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    
    // Time
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    // Location
    public Transform attackPos;
    public LayerMask enemyLayer;
    public float attackRange;
    public int damage;
    private bool hitInput;

    // animation
    public GameObject meleeAreaObject;
    public SpriteRenderer meleeSpriteRenderer;

    // Directional Attack
    public GameObject meleeDirObject;
    private PolygonCollider2D meleeDirPolyColl;
    public SpriteRenderer meleeDirSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damage = 1;
        timeBtwAttack = 0.0f;
        startTimeBtwAttack = 0.8f;

        if (meleeAreaObject == null)
        {
            meleeAreaObject = GameObject.Find("MeleeField");
        }
        // Optional: Auto-get the sprite renderer if not assigned in Inspector
        if (meleeSpriteRenderer == null)
        {
            meleeSpriteRenderer = meleeAreaObject.GetComponentInChildren<SpriteRenderer>();
        }
        meleeSpriteRenderer.enabled = false; // make sure it starts hidden


        // perform initial check for direction melee object
        if (meleeDirObject != null)
        {
            meleeDirPolyColl = meleeDirObject.GetComponentInChildren<PolygonCollider2D>();
        }
        if (meleeDirSpriteRenderer == null)
        {
            meleeDirSpriteRenderer = meleeDirObject.GetComponentInChildren<SpriteRenderer>();
        } else
        {
            meleeDirSpriteRenderer.enabled = false;
        }
        
        if(meleeDirPolyColl != null)
        {
            meleeDirPolyColl.enabled = false;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack > 0)
            timeBtwAttack -= Time.deltaTime;
    }
    
    private IEnumerator ShowSpriteForDuration(float duration)
    {
        if (meleeSpriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer not found");
            yield break;
        }

        meleeSpriteRenderer.enabled = true;
        yield return new WaitForSeconds(duration);
        // Debug.Log("Melee Sprite enabled");
        meleeSpriteRenderer.enabled = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {

        if (!context.performed) return; // only count action once with context.start

        if (timeBtwAttack <= 0.0f)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
            // for (int i = 0; i < enemiesToDamage.Length; i++)
            foreach (var enemy in enemiesToDamage)
            {
                // enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
            // Debug.Log($"Player attack performed");

            StartCoroutine(ShowSpriteForDuration(0.25f));

            timeBtwAttack = startTimeBtwAttack;
        }

    }

    public void OnDirAttack(InputAction.CallbackContext context)
    {
        // if (!context.performed) return;  // Only trigger once per press

        if (context.performed && timeBtwAttack <= 0)
        {
            StartCoroutine(PerformDirAttack());
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    private IEnumerator PerformDirAttack()
    {
        // Debug.Log("Player attack started");

        // Show sprite
        if (meleeDirSpriteRenderer != null)
            meleeDirSpriteRenderer.enabled = true;

        // Enable hitbox
        if (meleeDirPolyColl != null)
        {
            meleeDirPolyColl.enabled = true;

            // Use OverlapCollider to detect enemies currently inside
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(enemyLayer);

            Collider2D[] results = new Collider2D[10];
            int hits = meleeDirPolyColl.OverlapCollider(filter, results);

            for (int i = 0; i < hits; i++)
            {
                EnemyHealth enemy = results[i].GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    // Debug.Log($"Hit enemy: {enemy.name}");
                }
            }
        }

        // Wait for duration
        yield return new WaitForSeconds(0.4f);

        // Hide sprite and disable collider
        if (meleeDirSpriteRenderer != null)
            meleeDirSpriteRenderer.enabled = false;

        if (meleeDirPolyColl != null)
            meleeDirPolyColl.enabled = false;

        // Debug.Log("Player attack finished");
    }
    
    private void OnDrawGizmosSelected()
    {
        if(attackPos != null)
        {
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
            Gizmos.color = Color.red;
        }
    }

    // for opening doors / chests
    // public void Interact() {}

}
