using UnityEngine;

public class WaypointGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,1);
    }
}
