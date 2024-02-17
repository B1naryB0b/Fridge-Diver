using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [Header("Starting Stats")]
    [SerializeField] private int hunger;
    [SerializeField] private int thirst;
    [SerializeField] private int health;

    [Header("Vomit")]
    [SerializeField] private int hungerLoss;
    [SerializeField] private int thirstLoss;
    [SerializeField] private int healthLoss;

    [Header("Sliders")]
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider thirstSlider;
    [SerializeField] private Slider healthSlider;

    private bool isOvereating;

    private void Start()
    {
        DisplayValues();
    }

    private void DisplayValues()
    {
        hungerSlider.value = hunger;
        thirstSlider.value = thirst;
        healthSlider.value = health;
    }

    public void ApplyChanges(FoodStats stats)
    {
        hunger += stats.hungerDelta;
        thirst += stats.thirstDelta;
        health += stats.healthDelta;

        CheckOverEating();
        DisplayValues();
    }

    public void ApplyStatusEffect()
    {

    }

    private void CheckOverEating()
    {
        if (isOvereating)
        {
            Debug.Log("Vomit");
            Vomit();
        }

        if (hunger > hungerSlider.maxValue)
        {
            isOvereating = true;
        }
        else
        {
            isOvereating = false;
        }
    }

    private void Vomit()
    {
        hunger -= hungerLoss;
        thirst -= thirstLoss;
        health -= healthLoss;
    }
}
