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
    // public int damage;
    // private bool hitInput;

    // Area Attack animation
    public GameObject meleeAreaObject;
    public SpriteRenderer meleeSpriteRenderer;
    [SerializeField] private int areaDamage;

    // Directional Attack
    public GameObject meleeDirObject;
    private PolygonCollider2D meleeDirPolyColl;
    public SpriteRenderer meleeDirSpriteRenderer;
    [SerializeField] private int dirDamage;

    [SerializeField] private DirectionalHitbox currHitbox;
    private Direction currDir = Direction.North;

    // float boxAngle = 0; 
    // Vector2 boxSizeVector = new Vector2(3.25f, 1.7f); 
    // Vector2 weaponPos = new Vector2(0.0f, 0.0f); 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // damage = 1;
        areaDamage = 1;
        dirDamage = 1;
        
        timeBtwAttack = 0.0f;
        startTimeBtwAttack = 0.8f;

        // weaponPos = gameObject.transform.position;

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

        if(currHitbox == null)
        {
            GameObject tempObj = GameObject.Find("MeleeDirHitbox");
            currHitbox = tempObj.GetComponent<DirectionalHitbox>();
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

    private IEnumerator ShowDirMeleeForDuration(float duration)
    {
        // Debug.Log("Dir Attack Sprite Shown");
        if (meleeDirSpriteRenderer == null)
        {
            Debug.LogError("Dir Melee Sprite Renderer not found");
            yield break;
        }

        meleeDirSpriteRenderer.enabled = true;
        yield return new WaitForSeconds(duration);
        meleeDirSpriteRenderer.enabled = false;
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
                enemy.GetComponent<EnemyHealth>().TakeDamage(areaDamage);
            }
            // Debug.Log($"Player attack performed");

            StartCoroutine(ShowSpriteForDuration(0.25f));

            timeBtwAttack = startTimeBtwAttack;
        }

    }

    public void OnDirAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;  // Only trigger once per press
        
        // Debug.Log("Dir Attack");
        
        float boxAngle = 0;
        Vector2 boxSizeVector = new Vector2(3.25f, 1.7f);
        Vector2 weaponPos = attackPos.position;
        // boxAngle = 0;
        // boxSizeVector = new Vector2(3.25f, 1.7f);
        // weaponPos = attackPos.position;
        weaponPos.y += 1.7f;

        currDir = currHitbox.GetDirection();
        if( currDir == Direction.North )
        {
            boxSizeVector = new Vector2(3.25f, 1.7f);
            weaponPos = attackPos.position;
            weaponPos.y += 1.7f;
        } else if( currDir == Direction.East )
        {
            boxSizeVector = new Vector2(1.7f, 3.25f);
            weaponPos = attackPos.position;
            weaponPos.x += 1.7f;
        } 
        else if( currDir == Direction.South )
        {
            boxSizeVector = new Vector2(3.25f, 1.7f);
            weaponPos = attackPos.position;
            weaponPos.y -= 1.7f;
        } 
        else if( currDir == Direction.West )
        {
            boxSizeVector = new Vector2(1.7f, 3.25f);
            weaponPos = attackPos.position;
            weaponPos.x -= 1.7f;
        } 
        else if( currDir == Direction.NorthEast )
        {
            boxAngle = -45;
            boxSizeVector = new Vector2(3.25f, 1.7f);
            weaponPos = attackPos.position;
            weaponPos.x += 1.32f;
            weaponPos.y += 1.32f;
        } 
        else if( currDir == Direction.SouthEast )
        {
            boxAngle = 45;
            boxSizeVector = new Vector2(3.25f, 1.7f);
            weaponPos = attackPos.position;
            weaponPos.x += 1.32f;
            weaponPos.y -= 1.32f;
        } 
        else if( currDir == Direction.SouthWest )
        {
            boxAngle = -45;
            boxSizeVector = new Vector2(3.25f, 1.7f);
            weaponPos = attackPos.position;
            weaponPos.x -= 1.32f;
            weaponPos.y -= 1.32f;
        } 
        else if( currDir == Direction.NorthWest )
        {
            boxAngle = 45;
            boxSizeVector = new Vector2(3.25f, 1.7f);
            weaponPos = attackPos.position;
            weaponPos.x -= 1.32f;
            weaponPos.y += 1.32f;
        } 
        // Debug.Log($"Attack placed on {weaponPos}");

        if (timeBtwAttack <= 0.0f)
        {
            // Debug.Log("Attack Allowed");

            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(weaponPos, boxSizeVector, boxAngle, enemyLayer);
            foreach (var enemy in enemiesToDamage)
            {
                // Debug.Log("Found Enemy");
                enemy.GetComponent<EnemyHealth>().TakeDamage(dirDamage);
            }

            StartCoroutine(ShowDirMeleeForDuration(0.5f));
            timeBtwAttack = startTimeBtwAttack;

            // StartCoroutine(PerformDirAttack());
            // Debug.Log("Dir Attack Sprite Shown");
            
        }
        
    }

    
    private void OnDrawGizmosSelected()
    {
        // if(attackPos != null)
        // {
        //     Gizmos.DrawWireSphere(attackPos.position, attackRange);
        //     Gizmos.color = Color.red;
        // }
        if (attackPos != null)
        {

            // Gizmos.DrawWireSphere(attackPos.position, attackRange);
            // Gizmos.color = Color.red;

            // Save the current Gizmos matrix to restore it later
            // Matrix4x4 originalMatrix = Gizmos.matrix;

            // Set the matrix to match the position, rotation, and scale of your detection point
            // Gizmos.matrix = Matrix4x4.TRS(attackPos.position, Quaternion.Euler(0, 0, detectionAngle), Vector3.one);

            // Draw a wire cube for the Directional attack
            // Gizmos.DrawWireCube(weaponPos, boxSizeVector);

            // Restore the original Gizmos matrix
            // Gizmos.matrix = originalMatrix;
        }
    }
    

    // for opening doors / chests
    // public void Interact() {}

}
