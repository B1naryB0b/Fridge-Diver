using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FridgeManager : MonoBehaviour
{
    
    private List<GameObject> foods;

    public void ClearFridge()
    {
        foreach (GameObject food in foods)
        {
            Destroy(food);
        }
    }

    public void PopulateFridge()
    {
        
    }
}
