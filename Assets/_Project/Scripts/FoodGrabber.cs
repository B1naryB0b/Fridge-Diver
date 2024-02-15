using System.Collections.Generic;
using UnityEngine;

public class FoodGrabber : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private float destroyDistance = 0.5f;
    private List<Food> selectedFoods = new List<Food>();

    private void Update()
    {
        for (int i = selectedFoods.Count - 1; i >= 0; i--)
        {
            MoveToTarget(selectedFoods[i]);
        }
    }

    public void GrabFood(Food food)
    {
        if (!selectedFoods.Contains(food))
        {
            selectedFoods.Add(food);
            food.OnGrabbed();
        }
    }

    private void MoveToTarget(Food food)
    {
        food.transform.position = Vector3.Lerp(food.transform.position, targetTransform.position, lerpSpeed * Time.deltaTime);
        if (Vector3.Distance(food.transform.position, targetTransform.position) < destroyDistance)
        {
            food.EatFood();
            selectedFoods.Remove(food);
        }
    }
}
