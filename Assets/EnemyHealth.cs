using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Health Bar vars
    [SerializeField] private int health;
    
    public int fullHealth;
    public float percent;
    public GameObject healthBar;
    public HealthBar healthScript;
    // private GameObject bar;

    // Animation Vars
    [SerializeField] private Animator enemyAnimator;
    private float hitTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = fullHealth;
        percent = 1.0f;
        hitTimer = 0.0f;
        // GameObject currBar = new GameObject("Healthbar");
        // currBar.transform.position = Vector3.zero;
        Debug.Log("Setting up Enemy");
        if (healthBar == null)
        {
            // Instantiate(Healthbar, new Vector3(0, 0, 0), Quaternion.identity);

            // healthBar = new GameObject("Healthbar");
            // currHealth.transform.parent = null;

            // healthBar = GameObject.Find("Healthbar");
            // healthBar.localPosition.pos = Vector3.Zero();
            Debug.Log("Placing new Healthbar");
        }
        else
        {
            // healthScript.SetHealthObject(healthBar);
        }
        
        if (healthScript == null)
        {
            // healthScript = GameObject.Find("HealthScript");
            Debug.Log("No script attached");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTimer > 0f)
            hitTimer -= Time.deltaTime;
        else 
            enemyAnimator.SetBool("isHurt", false);
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
                Debug.Log("Kill Object");
            }
        }
        
        if(health <= 0)
        {
            enemyAnimator.SetBool("isDead", true);
            // Debug.Log("Dead Animation Triggered");
        } else
        {
            enemyAnimator.SetBool("isDead", false);
            enemyAnimator.SetBool("isHurt", true);
            hitTimer = 0.3f;
            // Debug.Log("Hurt Animation Triggered");
        }

        Debug.Log("Damage Taken");
    }

}
