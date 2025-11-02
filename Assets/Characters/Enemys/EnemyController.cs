using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator m_Animator;
    private EnemyCombat m_Combat;
    private EnemyWeapon m_Weapon;

    private int delay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = new EnemyAnimator(animator, spriteRenderer);
        m_Weapon = GetComponentInChildren<EnemyWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = LocatePlayer();

        if (player != null)
        {
            RotateTowardPlayer(player);
            m_Weapon.RotateWeapon(player);

            if (delay++ % 60 == 0)
                m_Weapon.Fire();
        }
    }

    private Transform LocatePlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 10, 1 << LayerMask.NameToLayer("Player"));

        if (player != null)
        {
            return player.gameObject.transform;
        }

        return null;
    }

    private void RotateTowardPlayer(Transform player)
    {
        Vector2 direction = (player.position - transform.position).normalized;

        Directional8 dir = DirectionalUtility.Get8Direction(direction);

        m_Animator.AnimateIdle(dir);
    }
}
