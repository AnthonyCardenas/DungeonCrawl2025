using UnityEngine;

public class HealthBarHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    // }

    // Update is called once per frame
    // void Update()
    // {
    // }

    [SerializeField] private HealthBar healthBar;


    private void Start()
    {
        // V2 - to automatically make a healthbar
        // HealthBar healthBar = HealthBar.Create(new Vector3(0f, 0f), new Vector3(1.65f, 0.15f)); // 1.65, 0.15
        // Debug.log("Created Scene HealthBar");
        
        // V1
        // float health = 1f;
        // healthBar.SetSize(.4f);

        // // FunctionPeriodic.Create()

        // healthBar.SetColor(Color.green);
    }

    void UpdateHealth(float currHealth)
    {
        healthBar.SetSize(currHealth);
        if(currHealth > 0.4)
        {
            healthBar.SetColor(Color.green);
        } else
        {
            healthBar.SetColor(Color.red);
        }
    }


}
