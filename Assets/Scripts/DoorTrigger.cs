using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public SpriteRenderer openDoorSprite; // Drag parent door sprite here
    public SpriteRenderer closedDoorSprite;

    public Collider2D doorCollider;

    private int playerLayer;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        // if(doorCollider == null)
        //     doorCollider = GetComponent<Collider2D>("DoorCollider");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            OpenDoor();
            // doorSprite.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            CloseDoor();
            // doorSprite.enabled = true;
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Opened door");
        // Populate adjacent room
        // Deactivate door sprite
        openDoorSprite.enabled = true;
        closedDoorSprite.enabled = false;
        // Deactivate door collider
        if(doorCollider != null)
        {
            doorCollider.enabled = false;
            Debug.Log("Collider Disabled");
        }
        
    }

    public void CloseDoor()
    {
        // Let player pass door frame
        // Reactivate door collider
        if(doorCollider != null)
        {
            doorCollider.enabled = true;
            Debug.Log("Collider Enabled");
        }
        // Reactivate door sprite
        openDoorSprite.enabled = false;
        closedDoorSprite.enabled = true;
    }
}
