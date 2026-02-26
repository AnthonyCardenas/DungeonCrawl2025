using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private int playerLayer;

    // public GameObject meleeField;
    public EnemyInteraction meleeInteraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerLayer = LayerMask.NameToLayer("Player");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            //Debug.Log("Player entered Enemy Range");
            meleeInteraction.SetPlayerInRange(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            //Debug.Log("Player exited Enemy Range");
            meleeInteraction.SetPlayerInRange(false);
        }
    }

    // public void ActiveDetection()
    // {
    //      // Enable hitbox
    //     if (meleeFieldPolyColl != null)
    //     {
    //         meleeFieldPolyColl.enabled = true;

    //         // Use OverlapCollider to detect players currently inside
    //         ContactFilter2D filter = new ContactFilter2D();
    //         filter.SetLayerMask(playerLayer);

    //         Collider2D[] results = new Collider2D[10];
    //         int hits = meleeFieldPolyColl.OverlapCollider(filter, results);

    //         for (int i = 0; i < hits; i++)
    //         {
    //             PlayerHealth player = results[i].GetComponent<PlayerHealth>();
    //             if (player != null)
    //             {
    //                 player.TakeDamage(damage);
    //                 Debug.Log($"Hit player: {player.name}");
    //             }
    //         }
    //     }
    // }
}
