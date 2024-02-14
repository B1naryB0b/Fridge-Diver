using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FridgeManager : MonoBehaviour
{
    [SerializeField] private int hunger;
    [SerializeField] private int thirst;
    [SerializeField] private int health;

    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider thirstSlider;
    [SerializeField] private Slider healthSlider;

    [SerializeField] private List<GameObject> foods;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        hungerSlider.maxValue = hunger;
        thirstSlider.maxValue = thirst;
        healthSlider.maxValue = health;
    }

    public void ApplyChanges(FoodStats stats)
    {
        hunger += stats.hungerDelta;
        thirst += stats.thirstDelta;
        health += stats.healthDelta;

        hungerSlider.value = hunger;
        thirstSlider.value = thirst;
        healthSlider.value = health;
    }
}
