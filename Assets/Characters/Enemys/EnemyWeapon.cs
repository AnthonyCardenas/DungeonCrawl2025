using TMPro;
using UnityEngine;

public enum WeaponType
{
    None,
    Melee,
    Range
}

public class EnemyWeapon : MonoBehaviour
{
    public WeaponType Type;
    public float Speed;
    public float Range;
    public float AttackPoints;
    public GameObject BulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateWeapon(Transform player)
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position;
        Destroy(bullet, 5);

        // Assuming 'q' is your quaternion and 'forward_3d' is your reference 3D direction
        Vector3 rotated_direction_3d = transform.rotation * Vector3.forward; // Or equivalent quaternion rotation operation

        // Project to 2D (e.g., in the XZ plane)
        Vector2 direction_2d = new Vector2(rotated_direction_3d.x, rotated_direction_3d.z);

        // Normalize if needed
        direction_2d = direction_2d.normalized;

        bullet.GetComponent<Rigidbody2D>().AddForce(direction_2d * Speed, ForceMode2D.Force);
    }
}
