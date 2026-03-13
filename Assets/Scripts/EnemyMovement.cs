using UnityEngine;
// using UnityEngine.Mathf;

public class EnemyMovement : MonoBehaviour
{
    public enum MoveState
    {
        Idle,
        Follow,
        Stop,
        ReturnHome,
        Dead,
        Stagger,
        IdleDebug
    }
    public MoveState currMove = MoveState.Idle;

    [SerializeField] private GameObject player;
    private Vector2 lastPlayerPos;
    // public float playerDist = 4.0f;
    private Vector2 homePos;

    private bool hasLineOfSight = false;
    private float timeLastSeen = 0.0f;
    private float deathTimer = 0.0f;
    // private bool inRange = false;
    private float speed = 2.5f;
    private float idleSpeed = 1.5f;

    public bool motionlessDebug = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // currMove = MoveState.Idle;
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if(lastPlayerPos == null)
            lastPlayerPos = player.transform.position;

        homePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        // called for every state
        timeLastSeen += Time.deltaTime;
        lineOfSight();
        updateState();
        if( (currMove == MoveState.Dead) || (currMove == MoveState.Stagger) )
        {
            deathTimer += Time.deltaTime;
        }
    }

    private void updateState ()
    {
        // Debug.Log($"Using {currMove} State");
        // movement state machine
        switch (currMove)
        {
            case MoveState.Follow:
                // Debug.Log("Using Follow State");
                // move towards the player
                moveTowardsPlayer();
                // if obstacle in way, use path planning

                // if player outside of range, switch to Stop (Not implemented)
                // if player out of line of sight for 2 secs, switch to Stop
                if( !hasLineOfSight && (timeLastSeen > 1.5f))
                {
                    currMove = MoveState.Stop;
                    // Debug.Log("Entering Stop State");
                }
                break;
            case MoveState.Idle:
                // Debug.Log("Using Idle State");
                // walk or stay around home position

                // if player is close enough, switch to Follow
                if(hasLineOfSight)
                {
                    currMove = MoveState.Follow;
                    // Debug.Log("Entering Follow State");
                }
                break;
            case MoveState.Stop:
                // Debug.Log("Using Stop State");
                // stay still

                // if player close enough, switch to Follow
                if(hasLineOfSight)
                {
                    currMove = MoveState.Follow;
                    // Debug.Log("Entering Follow State");
                }
                // if 2 secs have passed, switch to Return Home
                else if( timeLastSeen > 3.5f )
                {
                    currMove = MoveState.ReturnHome;
                    // Debug.Log("Entering Home State");
                }
                break;
            case MoveState.ReturnHome:
                // Debug.Log("Using Home State");
                // go to home position
                moveTowardsHome();

                float distToHome = calcVectorDistance(transform.position, homePos);
                // if player is close enough, switch to Follow
                if(hasLineOfSight)
                {
                    currMove = MoveState.Follow;
                    // Debug.Log("Entering Follow State");
                }
                // if at home location, switch to idle
                else if( distToHome < 0.5 && distToHome > -0.5)
                {
                    currMove = MoveState.Idle;
                    // Debug.Log("Entering Idle State");
                }
                break;
            case MoveState.Dead:
                // Debug.Log("Using Dead State");
                // Stay Still

                // if enemy is dead too long without killing object, switch to Idle
                if(deathTimer > 10.0f)
                {
                    currMove = MoveState.Idle;
                    // Debug.Log("Entering Idle State");
                }
                break;
            case MoveState.Stagger:
                // Debug.Log("Using Dead State");
                // Stay Still

                // if enemy is dead too long without killing object, switch to Idle
                if (motionlessDebug)
                {
                    currMove = MoveState.IdleDebug;
                    // Debug.Log("Entering IdleDebug State");
                }
                else if(deathTimer > 3.0f)
                {
                    currMove = MoveState.Idle;
                    // Debug.Log("Entering Idle State");
                }
                break;
            case MoveState.IdleDebug:
                // Debug.Log("Using IdleDebug State");
                // Stay Still. Used for Testing Hitboxes
                break;
            default:
                // currMove = MoveState.Idle;
                if ( motionlessDebug )
                    currMove = MoveState.IdleDebug;
                else
                    currMove = MoveState.IdleDebug;
                // Debug.Log("Entering Idle State from Default");
                break;
        }
    }

    // Only to be used to debug or change to dead state
    public void setMoveState(MoveState newState)
    {
        currMove = newState;
        if( (newState == MoveState.Dead) || (newState == MoveState.Stagger) )
        {
            deathTimer = 0.0f;
        }
    }

    public MoveState getMoveState()
    {
        return currMove;
    }


    private void moveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, lastPlayerPos, speed * Time.deltaTime);
    }

    private void moveTowardsHome()
    {
        transform.position = Vector2.MoveTowards(transform.position, homePos, idleSpeed * Time.deltaTime);
    }

    private void lineOfSight()
    {
        // Debug.Log("Finding Line of Sight");
        
        Vector2 rayDirection = player.transform.position - transform.position ;
        float rayDistance = calcDistance(transform.position.x, transform.position.y, player.transform.position.x, player.transform.position.y );
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, rayDirection, rayDistance );
        
        hasLineOfSight = false;
        // Loop through all the hits
        foreach (RaycastHit2D hit in hits)
        {
            // if enemy, skip eval (continue)
            if( hit.collider.tag == "Enemy" )
                continue;
            else if ( hit.collider.name == "MeleeTrigger" )
                continue;

            // Access information about the hit collider
            // Debug.Log("Hit object: " + hit.collider.name + " at distance: " + hit.distance);

            // if barrier return false, if player return true
            if(hit.collider.tag == "Player")
            {
                hasLineOfSight = true;
                Debug.DrawRay( transform.position, player.transform.position - transform.position , Color.green);
                lastPlayerPos = player.transform.position;
                // playerDist = rayDistance;
                timeLastSeen = 0.0f;
                // Debug.Log($"Line of sight: {hasLineOfSight}");
                break;
            } else
            {
                hasLineOfSight = false;
                Debug.DrawRay( transform.position, hit.collider.transform.position - transform.position , Color.red);
                // You can also access other components, e.g., changing the color
                // hit.collider.GetComponent<Renderer>().material.color = Color.red; 
                // Debug.Log($"Line of sight {hasLineOfSight}");
                break;
            }
            
            // Debug.DrawRay( transform.position, player.transform.position - transform.position , Color.red);
        }
    }

    public float calcDistance(float posAX, float posAY, float posBX, float posBY)
    {
        float dist = Mathf.Sqrt( Mathf.Pow(posAX - posBX, 2) + Mathf.Pow(posAY - posBY, 2) );
        return dist;
    }

    public float calcVectorDistance(Vector2 posA, Vector2 posB)
    {
        float dist = Mathf.Sqrt( Mathf.Pow(posA.x - posB.x, 2) + Mathf.Pow(posA.y - posB.y, 2) );
        return dist;
    }

    // public Vector2 calcDirection(float posAX, float posAY, float posBX, float posBY)
    // {
    //     // TODO: Learn how to normalize this vector
    //     Vector2 normVector = new Vector2( (posAX- posBX), (posAY - posBY));
    //     normVector.Normalize();
    //     return normVector;
    // }

    //private void AStarPathFinding() {}
}
