using UnityEngine;
using System.Collections;

public class SpawnedEgg : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private const float hiddenTime = 1.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        } 
        // else
        // {
        //     Debug.Log("Egg sprite not attached.");
        // }

        if(spriteRenderer != null)
        {
            // gameObject.SetActive(true);
            HideForDuration(hiddenTime);
        }
        
    }

    // Update is called once per frame
    // void Update()
    // {
    // }
    public void HideForDuration(float duration)
    {
        StartCoroutine(DelayShow(duration));
    }

    IEnumerator DelayShow(float delay)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(delay);
        spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player passed over the egg.");
            HideForDuration(2f);
            // player.CollectEgg("EggName");
        }
    }
}
