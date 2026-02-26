using UnityEngine;

public class DoorScript : MonoBehaviour
{

    private int playerLayer = 7;
    // public Collider DoorTriggerCollider;
    public SpriteRenderer OpenDoorSprite;
    public SpriteRenderer ClosedDoorSprite;

    // Directional Attack
    public GameObject doorIntObject;
    private PolygonCollider2D doorIntPolyColl;
    // public SpriteRenderer doorIntSpriteRenderer;

    public LayerMask layerMask;

    // doorIntPolyColl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");

        // perform initial check for direction melee object
        if (doorIntObject != null)
        {
            doorIntPolyColl = doorIntObject.GetComponentInChildren<PolygonCollider2D>();
        }
        // if (doorIntSpriteRenderer == null)
        // {
        //     doorIntSpriteRenderer = doorIntObject.GetComponentInChildren<SpriteRenderer>();
        // } else
        // {
        //     doorIntSpriteRenderer.enabled = false;
        // }
        
        if(doorIntPolyColl != null)
        {
            doorIntPolyColl.enabled = false;
        }
        // OpenDoorSprite = GetComponent<SpriteRenderer>("OpenDoorSprite");
        // ClosedDoorSprite = GetComponent<SpriteRenderer>("ClosedDoorSprite");
        // SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        // sprite.enabled = !sprite.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        // // Check for colliders within the sphere every frame
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, objectLayer);

        // foreach (var hitCollider in hitColliders)
        // {
        //     // Code to execute if an object on the specified layer is found within the area
        //     Debug.Log("Found an object inside the area: " + hitCollider.name, hitCollider.gameObject);
        // }
        
        // if(DoorTriggerCollider = true)
        // {
        //     OpenDoor();
        // }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            // Turn sprite OFF when player enters
            Debug.Log("Open door");
            OpenDoor();
            // doorSprite.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            // Turn sprite ON when player exits
            Debug.Log("Open door");
            CloseDoor();
            // doorSprite.enabled = true;
        }
    }

    // void OnTriggerEnter(Collider other) //(Collider DoorTriggerCollider)
    // {
    //     // layerMask = Player;
    //     // Check for colliders within the sphere every frame
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4, layerMask);

    //     foreach (var hitCollider in hitColliders)
    //     {
    //         // Code to execute if an object on the specified layer is found within the area
    //         Debug.Log("Found an object inside the area: " + hitCollider.name, hitCollider.gameObject);
    //     }
    //     // if(DoorTriggerCollider.CompareTag("Player"))
    //     // {
    //     //     // isObjectInTrigger = true;
    //     //     Debug.Log("Player entered the trigger");
    //     //     // OpenDoor();
    //     // }
    // }

    // private IEnumerator PerformOpenDoor()
    // {
    //     if(doorIntPolyColl != null)
    //     {
    //         doorIntPolyColl.enabled = true;

    //         // Use OverlapCollider to detect enemies currently inside
    //         ContactFilter2D filter = new ContactFilter2D();
    //         filter.SetLayerMask(playerLayer);

    //         Collider2D[] results = new Collider2D[10];
    //         int hits = doorIntPolyColl.OverlapCollider(filter, results);

    //         for (int i = 0; i < hits; i++)
    //         {
    //             // EnemyHealth enemy = results[i].GetComponent<EnemyHealth>();
    //             // if (enemy != null)
    //             // {
    //             //     enemy.TakeDamage(damage);
    //             //     Debug.Log($"Hit enemy: {enemy.name}");
    //             // }
    //             Debug.Log("Entered door trigger");
    //         }

    //     }
    // }

    // void OnTriggerExit(Collider DoorTriggerCollider)
    // {
    //     // if(DoorTriggerCollider.CompareTag("Player"))
    //     // {
    //     //     // isObjectInTrigger = false;
    //     //     Debug.Log("Player left the trigger");
    //     //     // CloseDoor();
    //     // }
    // }

    public void OpenDoor()
    {
        // Populate adjacent room
        // Deactivate door sprite
        OpenDoorSprite.enabled = true;
        ClosedDoorSprite.enabled = false;
        // Deactivate door collider
    }

    public void CloseDoor()
    {
        // Let player pass door frame
        // Reactivate door collider
        // Reactivate door sprite
        OpenDoorSprite.enabled = false;
        ClosedDoorSprite.enabled = true;
    }

}
