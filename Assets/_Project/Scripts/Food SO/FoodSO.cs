using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodSO : ScriptableObject
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

    public Sprite sprite;

    public FoodType foodType;

    public float hungerDelta;
    public float thirstDelta;
    public float healthDelta;

}
