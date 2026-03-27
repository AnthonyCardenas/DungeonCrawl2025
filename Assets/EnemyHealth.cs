using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Health Bar vars
    [SerializeField] private int health;
    public int fullHealth;
    public float percent = 1.0f;
    public GameObject healthBar;
    public HealthBar healthScript;
    // private GameObject bar;

    [SerializeField] private EnemyHandler currHandler;
    [SerializeField] private EnemyInteraction currInteraction;
    

    // Animation Vars
    [SerializeField] private Animator enemyAnimator;
    private float hitTimer = 0.0f;

    // Other Scripts 
    [SerializeField] private EnemyMovement currMovement;
    [SerializeField] private Collider2D currCollider;

    // public GameObject CurrEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Debug.Log("Setting up Enemy");
        health = fullHealth;
        // percent = 1.0f;
        // hitTimer = 0.0f; 
        
        // Health related objects
        if (healthScript == null)
        {
            healthScript = GetComponent<HealthBar>();
            //Debug.Log("No script attached");
        }
        if (healthBar == null)
        {
            healthBar = GameObject.Find("Healthbar (1)");
            // Debug.Log("Placing new Healthbar");
        }
        // Scripts
        if(currMovement == null)
        {
            currMovement = GetComponent<EnemyMovement>();
        }
        if (currCollider == null)
        {
            // currCollider = gameObject.collider;
            currCollider = GetComponent<CircleCollider2D>();
        }
        if(currHandler == null)
        {
            GameObject tempObject = GameObject.FindWithTag("GameHandler");
            if(tempObject != null)
            {
                currHandler = tempObject.GetComponent<EnemyHandler>();
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
        } else if (currMovement.getMoveState() == EnemyMovement.MoveState.Stagger)
        {
            currMovement.setMoveState( EnemyMovement.MoveState.Idle );
            enemyAnimator.SetBool("isHurt", false);
        }
        else
        {
            enemyAnimator.SetBool("isHurt", false);
        }
            
        // if (deathTimer > 1.0f)
        //     deathTimer -= Time.deltaTime;
        //     Destroy(this.GameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        percent = (float)health / (float)fullHealth;

        healthScript.SetSize(percent);
        
        if (percent < 0.5)
        {
            healthScript.SetColor(Color.red);

            if (percent <= 0.0f)
            {
                healthScript.Delete();
                // Debug.Log("Deleted Healthbar Object");
            }
        }
        
        if(health <= 0)
        {
            enemyAnimator.SetBool("isDead", true);
            currMovement.setMoveState(EnemyMovement.MoveState.Dead);
            currCollider.enabled = false;
            // Debug.Log("Counting enemy kill with enemy handler");
            // remove Enemy from Enemy Handler
            if(currHandler == null)
            {
                Debug.Log("No Enemy Handler Script attached");
            }
            else
            {
                currHandler.RemoveEnemy(); 
            }
            if(currInteraction == null)
            {
                Debug.Log("No Enemy Interaction Script attached");
            } 
            else
            {
                currInteraction.DeactivateAttack();
            }
            // Debug.Log("Dead Animation Triggered");
            Destroy(this.gameObject, 1.3f);
        } else
        {
            enemyAnimator.SetBool("isDead", false);
            enemyAnimator.SetBool("isHurt", true);
            hitTimer = 0.3f;
            currMovement.setMoveState(EnemyMovement.MoveState.Stagger);
            // Debug.Log("Hurt Animation Triggered");
        }

        // Debug.Log("Enemy took damage");
    }

}
