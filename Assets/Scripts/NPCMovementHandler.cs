using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class NPCMovementHandler : MonoBehaviour
{
    /// Enemy State Machine ///
    public enum MoveState
    {
        Idle,
        Follow,
        CloseFollow,
        Stop,
        ReturnHome,
        Dead,
        Stagger,
        IdleDebug
    }
    public MoveState currMove = MoveState.Idle;


    /// Key places/objects variables ///
    [SerializeField] private GameObject player;
    private Vector2 lastPlayerPos;
    private float distToPlayer;
    private Vector2 homePos;

    /// Timer state maching transitions ///
    private bool hasLineOfSight = false;
    private float timeLastSeen = 0.0f;
    private float deathTimer = 0.0f;
    // private bool inRange = false;
    private float speed = 2.5f;
    // private float idleSpeed = 1.5f;

    public bool motionlessDebug = false;


    /// Pathfinding Movement Handler Variables ///
    private float pathUpdateTimer = 0.0f; 
    private const float pathUpdatePeriod = 2.0f; // every 2 seconds
    private const float centeredDist = 2.0f;
    private int currPathIndex;
    private List<Vector3> pathVectorList;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure all objects like the player are uploaded into the script
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
        pathUpdateTimer += Time.deltaTime;
        checkLineOfSight();
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
            case MoveState.Follow: // move towards the player
                if(pathUpdateTimer > pathUpdatePeriod) // every x seconds update the best path
                {
                    Vector3 playerPosition = player.transform.position;
                    SetTargetPosition(playerPosition);
                    pathUpdateTimer = 0.0f;
                    // Debug.Log("Updated player position.");
                }
                
                moveTowardsPlayer();
                // if obstacle in way, use path planning

                // if player outside of range, switch to Stop (Not implemented)
                // if player within grid distance, go to CloseFollow
                distToPlayer = Vector3.Distance(player.transform.position, transform.position);
                // if player out of line of sight for 2 secs, switch to Stop
                if( !hasLineOfSight && (timeLastSeen > 1.5f))
                {
                    currMove = MoveState.Stop;
                    // Debug.Log("Entering Stop State from Follow State");
                } else if (distToPlayer < 5f)
                {
                    currMove = MoveState.CloseFollow;
                    // Debug.Log("Entering Close Follow State from Follow State");
                }
                break;
            case MoveState.CloseFollow:
                moveDirTowardsPlayer();
                // if obstacle in way, use path planning

                // if player outside of range, switch to Stop (Not implemented)
                // if player within grid distance, go to CloseFollow
                distToPlayer = Vector3.Distance(player.transform.position, transform.position);
                // if player out of line of sight for 2 secs, switch to Stop
                if( !hasLineOfSight && (timeLastSeen > 1.5f))
                {
                    currMove = MoveState.Stop;
                    // Debug.Log("Entering Stop State from Follow State");
                } else if (distToPlayer > 15f)
                {
                    currMove = MoveState.Follow;
                    // Debug.Log("Entering Follow State from Close Follow State");
                }
                break;
            case MoveState.Idle: // walk or stay around home position
                
                // if player is in line of sight, switch to Follow
                if(hasLineOfSight)
                {
                    currMove = MoveState.Follow;
                    Vector3 playerPosition = player.transform.position;
                    SetTargetPosition(playerPosition);
                    pathUpdateTimer = 0.0f;
                    // Debug.Log("Entering Follow State from Idle State");
                }
                break;
            case MoveState.Stop: // stay still
                
                // if player close enough, switch to Follow
                if(hasLineOfSight)
                {
                    currMove = MoveState.Follow;
                    Vector3 playerPosition = player.transform.position;
                    SetTargetPosition(playerPosition);
                    pathUpdateTimer = 0.0f;
                    // Debug.Log("Entering Follow State");
                }
                // if 2 secs have passed, switch to Return Home
                else if( timeLastSeen > 3.5f )
                {
                    currMove = MoveState.ReturnHome;
                    SetTargetPosition(homePos);
                    pathUpdateTimer = 0.0f;
                    // Debug.Log("Entering Home State");
                }
                break;
            case MoveState.ReturnHome:
                if(pathUpdateTimer > pathUpdatePeriod)
                {
                    // Debug.Log("Updated home position.");
                    SetTargetPosition(homePos);
                    pathUpdateTimer = 0.0f;
                }
                // go to home position
                moveTowardsHome();

                float distToHome = calcVectorDistance(transform.position, homePos);
                // if player is in line of sight, switch to Follow
                if(hasLineOfSight)
                {
                    currMove = MoveState.Follow;
                    Vector3 playerPosition = player.transform.position;
                    SetTargetPosition(playerPosition);
                    pathUpdateTimer = 0.0f;
                    // Debug.Log("Entering Follow State from Return Home state");
                }
                // if at home location, switch to idle
                else if( distToHome < 1.5 && distToHome > -1.5)
                {
                    currMove = MoveState.Idle;
                    // Debug.Log("Entering Idle State from ReturnHome state");
                }
                break;
            case MoveState.Dead: // Stay Still, Used to make dying animation not float
                
                // if enemy is dead too long without killing object, switch to Idle
                if(deathTimer > 10.0f)
                {
                    currMove = MoveState.Idle;
                    // Debug.Log("Entering Idle State from Dead");
                }
                break;
            case MoveState.Stagger: // Stay Still
                
                // if enemy is dead too long without killing object, switch to Idle
                if (motionlessDebug)
                {
                    currMove = MoveState.IdleDebug;
                    // Debug.Log("Entering IdleDebug State from Stagger");
                }
                else if(deathTimer > 3.0f)
                {
                    currMove = MoveState.Idle;
                    // Debug.Log("Entering Idle State from Stagger");
                }
                break;
            case MoveState.IdleDebug: // Stay Still. Used for Testing Hitboxes
                
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


    // move towards the player using the pathfinding script
    private void moveTowardsPlayer()
    {
        HandlePathFollowMovement();
    }

    // move directly towards the player
    private void moveDirTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void moveTowardsHome()
    {
        HandlePathFollowMovement();
    }

    private void checkLineOfSight()
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
                // Debug.Log($"Line of sight {hit.collider.name}");
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

    // Makes the object/character follow the predefined path
    // Need to set target position first
     private void HandlePathFollowMovement()
    {
        // Debug.Log("Entering Handle Movement func in PathfindingMovement");
        if(pathVectorList != null)
        {
            // Debug.Log($"Handling Index: {currPathIndex}");
            Vector3 targetPosition = pathVectorList[currPathIndex];
            // Debug.Log($"Target Position: {targetPosition}");

            float travelDist = Vector3.Distance(transform.position, targetPosition);
            // Debug.Log($"Distance to travel: {travelDist}");
            if( travelDist > centeredDist )
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                // Debug.Log($"Move Direction: {moveDir}");
                // animatedWalker.SetMoveVector(moveDir);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                // Debug.Log($"NPC moving towards {targetPosition}");
                
            } else
            {
                currPathIndex++;
                // Debug.Log($"NPC updated path.");
                if(currPathIndex >= pathVectorList.Count)
                {
                    // Debug.Log($"NPC stopped moving.");
                    StopMoving();
                    // animatedWalker.SetMoveVector(Vector3.zero);
                }
            }
        } else
        {
            // Debug.Log($"NPC path vector empty.");
            // Set the animation to 
            // animatedWalker.SetMoveVector(Vector3.zero);
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        if( !Pathfinding.Instance.InsideGrid(targetPosition) )
        {
            Debug.Log("Target(player/home) outside grid");
            return;
        }
        // reset path index
        currPathIndex = 0;
        // Get a list of vectors leading to the target position
        Vector3 startPosition = GetPosition();
        if( !Pathfinding.Instance.InsideGrid(startPosition) )
        {
            Debug.Log($"NPC outside grid at {startPosition}");
            return;
        }
        // Debug.Log($"Character's position: {GetPosition()}");
        // Debug.Log($"Target position: {targetPosition}");
        pathVectorList = Pathfinding.Instance.FindVPath(startPosition, targetPosition);

        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    // public Vector2 calcDirection(float posAX, float posAY, float posBX, float posBY)
    // {
    //     // TODO: Learn how to normalize this vector
    //     Vector2 normVector = new Vector2( (posAX- posBX), (posAY - posBY));
    //     normVector.Normalize();
    //     return normVector;
    // }
}
