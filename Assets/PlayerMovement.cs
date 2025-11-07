using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool m_KnockBackEffect = false;
    private float m_KnockBackDuration = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = moveInput* moveSpeed;

        if (m_KnockBackEffect == true)
        {
            m_KnockBackDuration += Time.deltaTime;
            if (m_KnockBackDuration >= 0.20f)
            {
                m_KnockBackEffect = false;
                m_KnockBackDuration = 0;
            }
        }
        else if (m_KnockBackEffect == false)
        {
            rb.linearVelocity = velocity;
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void TriggerKnockBackEffect()
    {
        m_KnockBackEffect = true;
    }

}
