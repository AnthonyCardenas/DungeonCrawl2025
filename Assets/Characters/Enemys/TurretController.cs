using UnityEngine;

public class TurretController : MonoBehaviour
{
    private TurretTrackingSystem m_TurretTrackingSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_TurretTrackingSystem = new TurretTrackingSystem(transform);
    }

    // Update is called once per frame
    void Update()
    {
        m_TurretTrackingSystem.RotateTowardsEnemy();
    }
}
