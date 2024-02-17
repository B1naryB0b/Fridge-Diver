using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private FridgeManager fridgeManager;
    [SerializeField] private DoorController doorController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private int foodCount;
    [SerializeField] private float turnDuration = 5f;

    private int turnCount;
    private bool turnStarted;
    private float turnTimer;

    void Start()
    {
        turnCount = 0;
        turnStarted = false;
        fridgeManager.PopulateFridge(foodCount);
    }

    void Update()
    {
        if (turnStarted)
        {
            turnTimer += Time.deltaTime;

            if (turnTimer >= turnDuration)
            {
                CloseFridge();
            }
        }
    }

    public void OpenFridge()
    {
        turnCount += 1;
        turnStarted = true;
        turnTimer = 0f; // Reset the timer when the turn starts
        cameraController.ZoomLevel(-10f);
    }

    private void CloseFridge()
    {
        turnStarted = false;
        turnTimer = 0f; // Reset the timer when the turn ends
        doorController.CloseDoor();
        cameraController.ZoomLevel(-15f);
        StartCoroutine(DelayPopulateFridge());
    }

    private IEnumerator DelayPopulateFridge()
    {
        yield return new WaitForSeconds(doorController.animationDuration);
        fridgeManager.PopulateFridge(foodCount);
    }
}
