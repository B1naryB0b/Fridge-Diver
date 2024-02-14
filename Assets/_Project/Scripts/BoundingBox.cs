using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    [SerializeField] private Vector3 boundingBoxSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private Color gizmoColor = Color.yellow;

    public Vector3 BoundingBoxSize => boundingBoxSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, boundingBoxSize);
    }
}
