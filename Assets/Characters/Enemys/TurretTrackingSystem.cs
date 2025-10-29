using UnityEngine;

public class TurretTrackingSystem
{
    private Transform m_TurretBase;
    private float m_RotateSpeed = 1;

    public TurretTrackingSystem(Transform turretBase)
    {
        m_TurretBase = turretBase;
    }

    public void RotateTowardsEnemy()
    {
        Collider2D player = Physics2D.OverlapCircle(m_TurretBase.position, 10, LayerMask.NameToLayer("Player"));

        if (player != null)
        {
            // Get the direction vector from this object to the target
            Vector3 direction = player.transform.position - m_TurretBase.position;

            // Calculate the angle in degrees relative to the X-axis
            // Atan2 is used for more accurate results across all quadrants
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the new rotation using Quaternion.Euler,
            // setting only the Z-axis and keeping X and Y at 0
            m_TurretBase.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            //m_TurretBase.Rotate(0, 0, 0);
        }
    }
}
