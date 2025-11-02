using UnityEngine;

public class EnemyAnimator
{
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    public EnemyAnimator(Animator animator, SpriteRenderer spriteRenderer)
    {
        m_Animator = animator;
        m_SpriteRenderer = spriteRenderer;
    }

    public void AnimateWalk(Directional8 dir)
    {
        AnimateIdle(dir);
        
    }

    public void AnimateIdle(Directional8 dir)
    {
        if (dir == Directional8.North || dir == Directional8.South ||
            dir == Directional8.East || dir == Directional8.NorthEast ||
            dir == Directional8.SouthEast)
            m_SpriteRenderer.flipX = false;
        else
            m_SpriteRenderer.flipX = true;

        if (dir == Directional8.North)
            m_Animator.Play("idle_up");
        else if (dir == Directional8.South)
            m_Animator.Play("idle_down");
        else if (dir == Directional8.West)
            m_Animator.Play("idle_right");
        else if (dir == Directional8.East)
            m_Animator.Play("idle_right");
        else if (dir == Directional8.NorthEast)
            m_Animator.Play("idle_up_right");
        else if (dir == Directional8.NorthWest)
            m_Animator.Play("idle_up_right");
        else if (dir == Directional8.SouthWest)
            m_Animator.Play("idle_down_right");
        else if (dir == Directional8.SouthEast)
            m_Animator.Play("idle_down_right");

        Debug.Log(dir.ToString());
    }
}
