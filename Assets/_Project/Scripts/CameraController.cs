using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 5.0f;
    [SerializeField] private float edgeThreshold = 0.2f; // Represents 1/5 of the screen
    [SerializeField] private float minY = -10.0f; // Minimum Y position
    [SerializeField] private float maxY = 10.0f; // Maximum Y position
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private float zoomSpeed = 1.0f;
    private float targetZoom;

    private void Start()
    {
        targetZoom = transform.position.z;
    }

    private void Update()
    {
        MoveCameraBasedOnMousePosition();
        PerformZoom();

    }

    private void MoveCameraBasedOnMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        float screenHeight = Screen.height;
        float moveDirection = 0f;

        if (mousePosition.y >= screenHeight * (1 - edgeThreshold))
        {
            float edgeRatio = (mousePosition.y - screenHeight * (1 - edgeThreshold)) / (screenHeight * edgeThreshold);
            moveDirection = speedCurve.Evaluate(edgeRatio);
        }
        else if (mousePosition.y <= screenHeight * edgeThreshold)
        {
            float edgeRatio = (screenHeight * edgeThreshold - mousePosition.y) / (screenHeight * edgeThreshold);
            moveDirection = -speedCurve.Evaluate(edgeRatio);
        }

        Vector3 newPosition = transform.position + Vector3.up * moveDirection * verticalSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }

    private void PerformZoom()
    {
        float newZ = Mathf.Lerp(transform.position.z, targetZoom, Time.deltaTime * zoomSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    public void ZoomLevel(float distance)
    {
        targetZoom = distance;
    }
}
