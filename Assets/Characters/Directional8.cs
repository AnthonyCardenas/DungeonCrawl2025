using UnityEngine;

public enum Directional8
{
    North,
    South,
    NorthEast,
    NorthWest,
    SouthEast,
    SouthWest,
    East,
    West
}

static public class DirectionalUtility
{
    static public Directional8 Get8Direction(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -22.5f && angle < 22.5f)    return Directional8.East;
        if (angle >= 22.5f && angle < 67.5f)     return Directional8.NorthEast;
        if (angle >= 67.5f && angle < 112.5f)    return Directional8.North;
        if (angle >= 112.5f && angle < 157.5f)   return Directional8.NorthWest;
        if (angle >= 157.5f || angle < -157.5f)  return Directional8.West;
        if (angle >= -157.5f && angle < -112.5f) return Directional8.SouthWest;
        if (angle >= -112.5f && angle < -67.5f)  return Directional8.South;
        if (angle >= -67.5f && angle < -22.5f)   return Directional8.SouthEast;

        return Directional8.South; // default fallback
    }

    static public float Get8DirectionAngle(Directional8 dir)
    {
        switch (dir)
        {
            case Directional8.East: return 0f;
            case Directional8.NorthEast: return 45f;
            case Directional8.North: return 90f;
            case Directional8.NorthWest: return 135f;
            case Directional8.West: return 180f;   // or -180f
            case Directional8.SouthWest: return -135f;
            case Directional8.South: return -90f;
            case Directional8.SouthEast: return -45f;
            default: return 0f;
        }
    }
}