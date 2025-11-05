using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    // Area
    // [SerializeField] float interactRange = 2f;
    // Time
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    // Location
    public Transform attackPos;
    public LayerMask enemyLayer;
    public float attackRange;
    public int damage;
    private bool hitInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damage = 1;
        timeBtwAttack = 0;
        startTimeBtwAttack = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
        hitInput = context.ReadValueAsButton();
        if (timeBtwAttack <= 0)
        {
            if (hitInput is true)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                    Debug.Log($"Attack action done: {hitInput}");
                }
            }

            timeBtwAttack = startTimeBtwAttack;
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    // public void Interact()
    // {
        
    //     Collider[] colliderArray = Physics.OverlapCircle(transform.position, interactRange);
    //     foreach( Collider collider in colliderArray)
    //     {
    //         Debug.log(collider);
    //     }
    // }
    
}
