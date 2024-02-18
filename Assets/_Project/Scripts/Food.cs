using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodStats stats;
    
    private Collider objectCollider;
    private Rigidbody objectRigidbody;

    [HideInInspector] public FoodGrabber grabber;
    [HideInInspector] public ResourceManager resourceManager;

    private void Start()
    {
        if (resourceManager == null) resourceManager = FindObjectOfType<ResourceManager>();
        if (grabber == null) grabber = FindObjectOfType<FoodGrabber>();

        objectCollider = GetComponent<Collider>();
        objectRigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        grabber.GrabFood(this);
    }

    public void OnGrabbed()
    {
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = true;
        }
    }

    public void EatFood()
    {

        resourceManager.ApplyChanges(stats);
        Destroy(gameObject);
        
    }
}
