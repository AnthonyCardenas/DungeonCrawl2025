using UnityEngine;

public class DirectionalHitbox : MonoBehaviour
{
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
    private Direction currDirection = Direction.North;

    [SerializeField] private GameObject meleeObject;

    public int damage = 1;

    void Start ()
    {
        if(meleeObject == null)
        {
            meleeObject = transform.parent.gameObject;
        }
    }

    public Direction GetDirection()
    {
        return currDirection;
    } 

    // private void OnTriggerEnter2D(Collision2D collision)
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Attacking from DirectionalHitbox script.");
    //     GameObject otherGameObject = collision.gameObject;
        
    //     if(otherGameObject.CompareTag("Enemy"))
    //     {
    //         // harm Enemy
    //         // if ( collision.TakeDamage())
    //         EnemyHealth enemy = otherGameObject.GetComponent<EnemyHealth>();
    //         if (enemy != null)
    //         {
    //             //enemy.TakeDamage(damage);
    //             Debug.Log("Damaged from new script.");
    //         }
    //     }
    // }

    public void changeMeleeDirection(Direction newDirection)
    {   
        currDirection = newDirection;
        //Debug.Log($"Direction Changed to {currDirection}");

        // Change direction of melee sprite and hitbox
        if(currDirection == Direction.North)
        {
             meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 180f); // }
            // meleeObject.transform.rotation.z = 180f; }

        } else if(currDirection == Direction.NorthEast)
        {
            meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 135f);
            // meleeObject.transform.rotation.z = 135f;
        } 
        else if(currDirection == Direction.East)
        {
            meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            // meleeObject.transform.rotation.z = 90f;
        } 
        else if(currDirection == Direction.SouthEast)
        {
            meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 45f);
            // meleeObject.transform.rotation.z = 45f;
        } 
        else if(currDirection == Direction.South)
        {
            meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            // meleeObject.transform.rotation.z = 0f;
        } 
        else if(currDirection == Direction.SouthWest)
        {
            meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 315f);
            // meleeObject.transform.rotation.z = 315f;
        } 
        else if(currDirection == Direction.West)
        {
            meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 270f);
            // meleeObject.transform.rotation.z = 270f;
        } 
        else if(currDirection == Direction.NorthWest)
        {
            meleeObject.transform.eulerAngles = new Vector3(0f, 0f, 225f);
            // meleeObject.transform.rotation.z = 225f;
        } 
        // else
        // {
        //    meleeObject.transform.rotation.z = 180f;
        // }

        // Change direction/position of melee hitbox

    }
}
