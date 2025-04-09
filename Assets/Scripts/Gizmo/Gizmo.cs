using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public enum GizmoShape 
	{ 
		Sphere,
		 Box,
		 Ellipse2D
	}

    public GizmoShape shape = GizmoShape.Sphere;
    public Color color = Color.red;

    public float radius = 0.5f; 
    public Vector3 boxSize = new Vector3(1f, 1f, 1f);
	public Vector2 ellipseRadius = new Vector2(1f, 0.5f); // For Ellipse2D
    public bool drawGizmo = true;
    public bool drawGizmoWire = true;
    public bool drawGizmoSolid = true;

    void OnDrawGizmos()
    {
        DrawGizmo();
    }

    void OnDrawGizmosSelected()
    {
        DrawGizmo();
    }

    private void DrawGizmo()
    {
        if (!drawGizmo) return;

        Gizmos.color = color;

        if (shape == GizmoShape.Sphere)
        {
            if (drawGizmoWire)
                Gizmos.DrawWireSphere(transform.position, radius);

            if (drawGizmoSolid)
                Gizmos.DrawSphere(transform.position, radius);
        }
        else if (shape == GizmoShape.Box)
        {
            if (drawGizmoWire)
                Gizmos.DrawWireCube(transform.position, boxSize);

            if (drawGizmoSolid)
                Gizmos.DrawCube(transform.position, boxSize);
        }
		else if (shape == GizmoShape.Ellipse2D)
		{
			Matrix4x4 prevMatrix = Gizmos.matrix;

			Gizmos.color = color;

			Matrix4x4 matrix = Matrix4x4.TRS(
				transform.position,
				Quaternion.identity,
				new Vector3(ellipseRadius.x, ellipseRadius.y, 1f) // 타원 형태 스케일
			);

			Gizmos.matrix = matrix;

			if (drawGizmoWire)
				Gizmos.DrawWireSphere(Vector3.zero, 1f);

			if (drawGizmoSolid)
				Gizmos.DrawSphere(Vector3.zero, 1f);

			Gizmos.matrix = prevMatrix;
		}
    }
}
