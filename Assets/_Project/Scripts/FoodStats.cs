using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodStats
{
    public enum FoodType
    {
        Dairy,
        Fruit,
        Vegetable,
        Meat,
        Carbohydrates,
        Fats,
        Sugars
    }

    public FoodType foodType;

    public int hungerDelta;
    public int thirstDelta;
    public int healthDelta;

    public enum StatusEffect
    {
        None,
        Spicy,
        Energising,
        Soothing,
        Intoxicating
    }

    public StatusEffect statusEffect;

}
