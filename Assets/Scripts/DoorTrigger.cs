using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public SpriteRenderer openDoorSprite; // Drag parent door sprite here
    public SpriteRenderer closedDoorSprite;

    public Collider2D doorCollider;

    private int playerLayer;

    [SerializeField] private DoorState currDoorState = DoorState.Locked;
    // [SerializeField] private DoorState currDoorState;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");

        if(currDoorState == null)
            currDoorState = DoorState.Locked;
        
        closedDoorSprite.enabled = true;
        openDoorSprite.enabled = true;
        // if(doorCollider == null)
        // {
        //     doorCollider = GetComponent<Collider2D>();
        //     Debug.Log($"Found Collider {doorCollider.name}");
        // } else
        // {
        //     Debug.Log($"No Collider Found");
        // }
            
    }

    public DoorState GetDoorState()
    {
        return currDoorState;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            
            if (currDoorState == DoorState.Unlocked)
            {
                // Debug.Log("Door is Unlocked. Opening door.");
                OpenDoor();
            }
            else if (currDoorState == DoorState.Locked)
            {
                // Debug.Log("Door is Locked. Can't open.");
            }
            else if (currDoorState == DoorState.Open)
            {
                // Debug.Log("Door is already open.");
            }
            // Debug.Log("Door is not connected to Game Enemy Handler.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            if (currDoorState == DoorState.Open)
            {
                // Debug.Log("Closing the open door");
                CloseDoor();
            }
            else if (currDoorState == DoorState.Locked)
            {
                // Debug.Log("Door is Locked leaving door area.");
            }
        }
    }

    public void LockDoor()
    {
        // Debug.Log("Locking door");
        currDoorState = DoorState.Locked;
    }

    public void UnlockDoor()
    {
        //Debug.Log("Unlocking door");
        currDoorState = DoorState.Unlocked;
    }

    public void OpenDoor()
    {
        // Debug.Log("Opened door");

        // Deactivate door collider
        if(doorCollider != null)
        {
            doorCollider.enabled = false;
            // Debug.Log("Collider Disabled");
        }
        // Deactivate door sprite
        openDoorSprite.enabled = true;
        closedDoorSprite.enabled = false;

        currDoorState = DoorState.Open;
    }

    public void CloseDoor()
    {
        // Let player pass door frame
        // Reactivate door collider
        if(doorCollider != null)
        {
            doorCollider.enabled = true;
            // Debug.Log("Collider Enabled");
        }
        // Reactivate door sprite
        openDoorSprite.enabled = false;
        closedDoorSprite.enabled = true;

        currDoorState = DoorState.Unlocked;
    }
}
