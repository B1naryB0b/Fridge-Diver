using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private FoodStats stats;
    public Transform targetTransform;
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private float destroyDistance = 0.5f;

    private ResourceManager resourceManager;
    private bool isMovingToTarget = false;
    private Collider objectCollider;
    private Rigidbody objectRigidbody;

    private void Start()
    {
        if (resourceManager == null) resourceManager = FindObjectOfType<ResourceManager>();

        objectCollider = GetComponent<Collider>();
        objectRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isMovingToTarget)
        {
            MoveToTarget();
        }
    }

    private void OnMouseDown()
    {
        isMovingToTarget = true;
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = true;
        }
    }

    private void MoveToTarget()
    {
        if (targetTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, lerpSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetTransform.position) < destroyDistance)
            {
                EatFood();
            }
        }
    }

    private void EatFood()
    {
        if (gameObject.CompareTag("Food"))
        {
            resourceManager.ApplyChanges(stats);
            Destroy(gameObject);
        }
    }
}
