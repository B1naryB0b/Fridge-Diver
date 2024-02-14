using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodBankDataSO : ScriptableObject
{
    public string folderPath = "Assets/_Project/Prefabs/Food";
    public List<GameObject> foodPrefabs;

}
