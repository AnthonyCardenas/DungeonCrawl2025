using UnityEngine;

public class DirectionalHitbox : MonoBehaviour
{
    public int damage = 1;
    // private void OnTriggerEnter2D(Collision2D collision)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Attacking from new script.");
        GameObject otherGameObject = collision.gameObject;
        
        if(otherGameObject.CompareTag("Enemy"))
        {
            // harm Enemy
            // if ( collision.TakeDamage())
            EnemyHealth enemy = otherGameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                //enemy.TakeDamage(damage);
                Debug.Log("Damaged from new script.");
            }
        }
    }
}
