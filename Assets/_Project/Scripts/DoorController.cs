using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform doorTransform;
    [SerializeField] private float openAngle = 90f;
    public float animationDuration = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private float currentLerpTime;
    private bool isOpening;
    private bool isClosing;

    private void Start()
    {
        if (doorTransform == null)
        {
            doorTransform = transform;
        }

        closedRotation = doorTransform.rotation;
        openRotation = Quaternion.Euler(doorTransform.eulerAngles.x, doorTransform.eulerAngles.y + openAngle, doorTransform.eulerAngles.z);
    }

    private void Update()
    {
        if (isOpening)
        {
            AnimateDoor(openRotation);
        }
        else if (isClosing)
        {
            AnimateDoor(closedRotation);
        }
    }

    private void AnimateDoor(Quaternion targetRotation)
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > animationDuration)
        {
            currentLerpTime = animationDuration;
        }

        float perc = currentLerpTime / animationDuration;
        doorTransform.rotation = Quaternion.Lerp(doorTransform.rotation, targetRotation, perc);

        if (perc >= 1.0f)
        {
            isOpening = false;
            isClosing = false;
        }
    }

    private void OnMouseDown()
    {
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        if (!isOpening && !isClosing)
        {
            if (doorTransform.rotation == closedRotation)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    public void OpenDoor()
    {
        if (doorTransform.rotation != openRotation)
        {
            isOpening = true;
            isClosing = false;
            currentLerpTime = 0f;
        }
    }

    public void CloseDoor()
    {
        if (doorTransform.rotation != closedRotation)
        {
            isClosing = true;
            isOpening = false;
            currentLerpTime = 0f;
        }
    }
}
