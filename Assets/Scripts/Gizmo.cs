using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    [Header("Gizmo is not visible at runtime")]
    public Vector3 gizmoSize;
    public Color gizmoColor = new (0, 1, 0, 0.5f);
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor; 
        Gizmos.DrawCube(transform.position, gizmoSize);
    }
}
