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
    // [SerializeField] private DirectionalHitbox currHitbox;
    public DirectionalHitbox currHitbox;

    // public enum Direction {
    //     North,
    //     NorthEast,
    //     East,
    //     SouthEast,
    //     South,
    //     SouthWest,
    //     West,
    //     NorthWest
    // }
    public Direction currDir; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currDir = Direction.North;
        // if(currHitbox == null)
        // {
        //     currHitbox = GameObject.GetComponent<DirectionalHitbox>();
        // }
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

        if (!context.performed) return;

        if( (moveInput.y > 0) && (moveInput.x > 0) )
        {
            currDir = Direction.NorthEast;
        } else if ( (moveInput.y < 0) && (moveInput.x > 0) )
        {
            currDir = Direction.SouthEast;
        } else if ( moveInput.x > 0 )
        {
            currDir = Direction.East;
        } else if ( (moveInput.y > 0) && (moveInput.x < 0) )
        {
            currDir = Direction.NorthWest;
        } else if ( (moveInput.y < 0) && (moveInput.x < 0) )
        {
            currDir = Direction.SouthWest;
        } else if ( moveInput.x < 0 )
        {
            currDir = Direction.West;
        } else if ( moveInput.y < 0 )
        {
            currDir = Direction.South;
        } else if ( moveInput.y > 0 )
        {
            currDir = Direction.North;
        } 
        // else // 0,0 or no movement
        // {
        //     currDir = Direction.North;
        // }
        // Debug.Log($"Move Input: {currDir}");
        
        currHitbox.changeMeleeDirection(currDir); 
        //Debug.Log(moveInput);
        //currDir = moveInput;
    }

    public void TriggerKnockBackEffect()
    {
        m_KnockBackEffect = true;
    }

}
