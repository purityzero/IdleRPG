using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(GizmoColliderBinder2D))]
public class GizmoColliderBinder2DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GizmoColliderBinder2D binder = (GizmoColliderBinder2D)target;

        if (GUILayout.Button("콜라이더 갱신"))
        {
			binder.UpdateCollider();
        }
    }
}

public class GizmoColliderBinder2D : MonoBehaviour
{
	public Action<Collider2D> OnTrigger;

	void OnTriggerEnter2D(Collider2D other)
	{
		OnTrigger?.Invoke(other);
	}

#if UNITY_EDITOR
	public Gizmo targetGizmo;

    private Gizmo.GizmoShape m_PreviousShape;
    private float m_PreviousRadius;
    private Vector3 m_PreviousBoxSize;
    private Vector3 m_PreviousPosition;
    private Vector2 m_PreviousEllipseRadius;

    void OnEnable()
    {
        if (targetGizmo == null) return;

        m_PreviousShape = targetGizmo.shape;
        m_PreviousRadius = targetGizmo.radius;
        m_PreviousBoxSize = targetGizmo.boxSize;
        m_PreviousPosition = transform.position;
        m_PreviousEllipseRadius = targetGizmo.ellipseRadius;
    }

    public void UpdateCollider()
    {
        Vector2 offset = targetGizmo.transform.position - transform.position;

        switch (targetGizmo.shape)
        {
            case Gizmo.GizmoShape.Sphere:
             {
				var circle = gameObject.GetAddComponent<CircleCollider2D>();
				circle.radius = targetGizmo.radius;
				circle.offset = offset;
				DestroyIfExists<BoxCollider2D>();
				DestroyIfExists<CapsuleCollider2D>();
				break;
            }
            case Gizmo.GizmoShape.Box:
            {
				DestroyIfExists<CircleCollider2D>();
				DestroyIfExists<CapsuleCollider2D>();

				var box = gameObject.GetAddComponent<BoxCollider2D>();
				box.size = targetGizmo.boxSize;
				box.offset = offset;
				break;
             }

            case Gizmo.GizmoShape.Ellipse2D:
            {
				DestroyIfExists<CircleCollider2D>();
				DestroyIfExists<BoxCollider2D>();

				var capsule = gameObject.GetAddComponent<CapsuleCollider2D>();
				capsule.size = targetGizmo.ellipseRadius * 2f;
				capsule.offset = offset;

				capsule.direction = targetGizmo.ellipseRadius.x >= targetGizmo.ellipseRadius.y
				? CapsuleDirection2D.Horizontal : CapsuleDirection2D.Vertical;

				break;
            }
        }
    }

    private void DestroyIfExists<T>() where T : Component
    {
        var comp = GetComponent<T>();
        if (comp != null)
            DestroyImmediate(comp);
    }
	
#endif
}

public static class GameObjectExtensions
{
	public static T GetAddComponent<T>(this GameObject obj) where T : Component
	{
		if (obj.GetComponent<T>() == null)
		{
			return obj.AddComponent<T>();
		}
		else
		{
			return obj.GetComponent<T>();
		}
	}
}