using UnityEngine;

public class DoorScript : MonoBehaviour
{

    // [SerializeField] private DoorState currDoorState = DoorState.Locked;

    // private int playerLayer = 7;
    // // public Collider DoorTriggerCollider;
    // public SpriteRenderer OpenDoorSprite;
    // public SpriteRenderer ClosedDoorSprite;

    // // Directional Attack
    // public GameObject doorIntObject;
    // private PolygonCollider2D doorIntPolyColl;

    // public LayerMask layerMask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // playerLayer = LayerMask.NameToLayer("Player");
        // currDoorState = DoorState.Locked;

        // // perform initial check for direction melee object
        // if (doorIntObject != null)
        // {
        //     doorIntPolyColl = doorIntObject.GetComponentInChildren<PolygonCollider2D>();
        // }
        
        // if(doorIntPolyColl != null)
        // {
        //     doorIntPolyColl.enabled = false;
        // }

    }

    // Update is called once per frame
    void Update()
    {
    }

    // private void doorStateMachine()
    // {
    //      switch (currDoorState)
    //     {
    //         case DoorState.Open:
    //             // Debug.Log("Using Open State");
    //             // Let player pass through
    //             // if player entered new room with enemies, lock door
    //             // if player backed away from door, switch to unlocked door
    //             break;
    //         case DoorState.Locked:
    //             // Debug.Log("Using Open State");
    //             // Block Player passage
    //             // if player eliminated all enemies, unlocklock door
    //             break;
    //         case DoorState.Unlocked:
    //             // Debug.Log("Using Open State");
    //             // Show Closed door
    //             // if player gets close enough, open door
    //             break;
    //         default:
    //             currDoorState = DoorState.Unlocked;
    //             break;
    //     }
    // }

    // public void SetDoorState(DoorState nextDoorState)
    // {
    //     currDoorState = nextDoorState;
    // }
    // public DoorState GetDoorState()
    // {
    //     return currDoorState;
    // }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.layer == playerLayer)
    //     {
    //         if(currDoorState == DoorState.Locked)
    //         {
    //             Debug.Log("Door is locked");
    //         }
    //         if(currDoorState == DoorState.Unlocked)
    //         {
    //             Debug.Log("Door is unlocked");
    //             currDoorState = DoorState.Open;
    //             Debug.Log("Open door");
    //             OpenDoor();
    //         }
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.layer == playerLayer)
    //     {
    //         if(currDoorState == DoorState.Open)
    //         {
    //             currDoorState = DoorState.Unlocked;
    //             Debug.Log("Close door");
    //             CloseDoor();
    //         }
    //     }
    // }

    // public void OpenDoor()
    // {
    //     OpenDoorSprite.enabled = true;
    //     ClosedDoorSprite.enabled = false;
    // }

    // public void CloseDoor()
    // {
    //     OpenDoorSprite.enabled = false;
    //     ClosedDoorSprite.enabled = true;
    // }

    // public void LockDoor()
    // {
    //     currDoorState = DoorState.Locked;
    // }

    // public void UnlockDoor()
    // {
    //    currDoorState = DoorState.Unlocked;
    // }

}
