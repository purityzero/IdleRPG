using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoundsGizmo : MonoSingleton<MovementBoundsGizmo>
{
    public Vector2 minBounds = new Vector2(-6f, -2f);
    public Vector2 maxBounds = new Vector2(6f, 2f);
    public Color gizmoColor = Color.magenta;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Vector3 center = (minBounds + maxBounds) * 0.5f;
        Vector3 size = maxBounds - minBounds;
        Gizmos.DrawWireCube(center, size);
    }

    public bool IsOutOfBounds(Vector2 position)
    {
        return position.x < minBounds.x ||
               position.x > maxBounds.x ||
               position.y < minBounds.y ||
               position.y > maxBounds.y;
    }
}
