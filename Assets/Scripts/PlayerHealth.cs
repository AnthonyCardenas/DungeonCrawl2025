using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Health numbers
    public int fullHealth;
    public int health;
    public float percent;

    
    // Health visuals
    public GameObject healthBar;
    public HealthBar healthBarScript;

    // Animation
    [SerializeField] private Animator playerAnimator;
    private float hurtTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
        //Debug.Log("Setting up Player Health");
        health = fullHealth; 
        percent = 1.0f; 
        hurtTimer = 0.0f; 

        if (healthBar == null)
        {
            Debug.Log("No healthbar object attached");
            // healthBar = GameObject.Find("HealthBar");
        }
        if (healthBarScript == null)
        {
            Debug.Log("No script attached");
            // healthBarScript = GetComponent<HealthBar>();
        }
        if(playerAnimator == null)
        {
            Debug.Log("No player animation attached");
            // playerAnimator = GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (hurtTimer > 0f)
            hurtTimer -= Time.deltaTime;
        else
        {
            playerAnimator.SetBool("isDead", false);
            playerAnimator.SetBool("isHurt", false);
        }
            
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("Player Taking Damage");
        health -= damage;
        percent = (float)health / (float)fullHealth;

        healthBarScript.SetSize(percent);
        
        if (percent < 0.5)
        {
            healthBarScript.SetColor(Color.red);

            if (percent <= 0.0f)
            {
                // Debug.Log("Delete HealthBar");
                // healthBarScript.Delete();
            }
        }
        
        if(health <= 0)
        {
            playerAnimator.SetBool("isDead", true);
            hurtTimer = 0.3f;
            //Debug.Log("Dead Player Animation Triggered");
            // Destroy(this.gameObject, 1.3f);
            // End Game Condition Met??
            // Respawn at beginning of room

        } else
        {
            playerAnimator.SetBool("isDead", false);
            playerAnimator.SetBool("isHurt", true);
            hurtTimer = 0.3f;
            // Debug.Log("Hurt Animation Triggered");
        }

        // Debug.Log($"Damage Taken {damage}");
    }
}
