using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private FoodStats stats;

    private FridgeManager manager;

    private void Start()
    {
        manager = FindFirstObjectByType<FridgeManager>();
    }

    private void OnMouseDown()
    {
        EatFood();
    }

    private void EatFood()
    {
        if (gameObject.CompareTag("Food"))
        {
            manager.ApplyChanges(stats);
            Destroy(gameObject);
        }
    }
}
