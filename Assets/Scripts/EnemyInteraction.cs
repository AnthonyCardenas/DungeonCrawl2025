using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class EnemyInteraction : MonoBehaviour
{

    //Timing
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    // Location
    public LayerMask playerLayer;
    public Transform attackPos;
    public float attackRange;
    public int damage;

    public bool playerInRange;

    // Art
    public SpriteRenderer meleeSpriteRenderer;

    // Player Health 
    // private PlayerHealth currPlayerHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerInRange = false; 

        if (damage == 0)
            damage = 1;

        if(attackRange == 0f)
            attackRange = 1.2f;
        
        if(playerLayer.value == 0)
            playerLayer.value = 7;
            //playerLayer = LayerMask.NameToLayer("Player");
        
        timeBtwAttack = 0.0f; 
        startTimeBtwAttack = 1.5f; 
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check attack timer
        if (timeBtwAttack > 0)
            timeBtwAttack -= Time.deltaTime;
        else if (playerInRange)
        {
            StartCoroutine(ShowSpriteForDuration(0.5f));
            PerformAttack();
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    public void SetPlayerInRange(bool present)
    {
        playerInRange = present;
    }

     public void PerformAttack()
    {
        // Debug.Log("Enemy Performing Attack");
        // Debug.Log($"Enemy Attack range {attackRange}");
        Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerLayer);

        if(playersToDamage == null)
        {
            Debug.Log("Players not found");
        }
        foreach (var player in playersToDamage)
        {
            // Debug.Log($"Found player: {player}");
            if(player.GetComponent<PlayerHealth>() == null )
            {
                Debug.Log("Player health not found");
            }
            else
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            
        }
        
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
}
