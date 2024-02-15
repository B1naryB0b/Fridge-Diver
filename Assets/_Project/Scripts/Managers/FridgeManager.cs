using System.Collections.Generic;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{
    [SerializeField] private FoodBankDataSO foodBankData;
    [SerializeField] private PhysicMaterial zeroFrictionMaterial;
    [SerializeField] private int minimumFoodCount;
    [SerializeField] private int maximumFoodCount;

    [SerializeField] private GameObject boundingBoxParent;

    [SerializeField] private FoodGrabber grabberReference;
    [SerializeField] private ResourceManager resourceManagerReference;

    private List<GameObject> foodsInFridge = new List<GameObject>();
    private List<BoundingBox> fridgeBoundingBoxes = new List<BoundingBox>();

    private void Awake()
    {
        foodsInFridge = new List<GameObject>();
        fridgeBoundingBoxes.AddRange(boundingBoxParent.GetComponentsInChildren<BoundingBox>());
    }

    public void ClearFridge()
    {
        foreach (GameObject food in foodsInFridge)
        {
            Destroy(food);
        }
        foodsInFridge.Clear();
    }

    public void PopulateFridge(int foodCount)
    {
        ClearFridge();

        foodCount = Mathf.Clamp(foodCount, minimumFoodCount, maximumFoodCount);
        int boundingBoxCount = fridgeBoundingBoxes.Count;

        if (boundingBoxCount == 0)
        {
            return;
        }

        int foodPerBox = foodCount / boundingBoxCount;
        int remainingFood = foodCount % boundingBoxCount;

        foreach (BoundingBox boundingBox in fridgeBoundingBoxes)
        {
            PopulateBoundingBoxWithFood(boundingBox, foodPerBox, ref remainingFood);
        }
    }

    private void PopulateBoundingBoxWithFood(BoundingBox boundingBox, int foodPerBox, ref int remainingFood)
    {
        int foodToInstantiate = foodPerBox + (remainingFood > 0 ? 1 : 0);
        if (remainingFood > 0)
        {
            remainingFood--;
        }

        for (int i = 0; i < foodToInstantiate; i++)
        {
            InstantiateFoodAtRandomPosition(boundingBox);
        }
    }

    private void InstantiateFoodAtRandomPosition(BoundingBox boundingBox)
    {
        Vector3 randomPosition = GetRandomPositionInsideBoundingBox(boundingBox);
        GameObject foodPrefab = GetRandomFoodPrefab();
        GameObject foodItem = Instantiate(foodPrefab, randomPosition, Random.rotation, transform);
        PrepareFoodItem(foodItem);
        foodsInFridge.Add(foodItem);
    }



    private Vector3 GetRandomPositionInsideBoundingBox(BoundingBox boundingBox)
    {
        Vector3 boxSize = boundingBox.BoundingBoxSize;
        return new Vector3(
            Random.Range(-boxSize.x / 2, boxSize.x / 2),
            Random.Range(-boxSize.y / 2, boxSize.y / 2),
            Random.Range(-boxSize.z / 2, boxSize.z / 2)
        ) + boundingBox.transform.position;
    }

    private GameObject GetRandomFoodPrefab()
    {
        if (foodBankData.foodPrefabs.Count == 0)
        {
            Debug.LogError("FoodBankDataSO does not contain any food prefabs.");
            return null;
        }
        return foodBankData.foodPrefabs[Random.Range(0, foodBankData.foodPrefabs.Count)];
    }

    private void PrepareFoodItem(GameObject foodItem)
    {
        Rigidbody rb = foodItem.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = foodItem.AddComponent<Rigidbody>();
        }
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Collider collider = foodItem.GetComponent<Collider>();
        collider.material = zeroFrictionMaterial;

        Food food = foodItem.GetComponent<Food>();
        food.grabber = grabberReference;
        food.resourceManager = resourceManagerReference;
    }
}
