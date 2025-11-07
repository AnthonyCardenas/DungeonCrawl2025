using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_RigiBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_RigiBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;


        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector2 direction = (collision.transform.position - transform.position).normalized;
        rb.gameObject.GetComponent<PlayerMovement>().TriggerKnockBackEffect();
        rb.AddForce(direction * 12f, ForceMode2D.Impulse);
    }
}
