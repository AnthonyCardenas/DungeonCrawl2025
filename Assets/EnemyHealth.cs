using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    public int fullHealth;
    public float percent;
    public GameObject healthBar;
    public HealthBar healthScript;
    // private GameObject bar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = fullHealth;
        // GameObject currBar = new GameObject("Healthbar");
        // currBar.transform.position = Vector3.zero;
        Debug.Log("Setting up Enemy");
        if (healthBar == null)
        {
            // Instantiate(Healthbar, new Vector3(0, 0, 0), Quaternion.identity);

            // healthBar = new GameObject("Healthbar");
            // currHealth.transform.parent = null;

            // healthBar = GameObject.Find("Healthbar");
            // healthBar.localPosition.pos = Vector3.Zero();
            Debug.Log("Placing new Healthbar");
        }
        else
        {
            // healthScript.SetHealthObject(healthBar);
        }
        
        if (healthScript == null)
        {
            // healthScript = GameObject.Find("HealthScript");
            Debug.Log("No script attached");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        float percent = (float)health / (float)fullHealth;

        healthScript.SetSize(percent);

        // bar.localScale.x = 0.5;
        // healthBar.SetSize(0.5);
        Debug.Log("Damage Taken");
    }

}
