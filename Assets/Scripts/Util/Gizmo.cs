using UnityEngine;

namespace Util
{
    public class Gizmo : MonoBehaviour
    {
        [Header("Gizmo is not visible at runtime")]
        public Vector3 gizmoSize;
        public Color gizmoColor = new (0, 1, 0, 0.5f);
        public bool showGizmo = true; 
    
        private void OnDrawGizmos()
        {
            if (!showGizmo) return;
            Gizmos.color = gizmoColor; 
            Gizmos.DrawCube(transform.position, gizmoSize);

        }
    }
}
