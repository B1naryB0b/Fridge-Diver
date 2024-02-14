using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(FoodBankDataSO))]
public class FoodBankDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FoodBankDataSO foodBankDataSO = (FoodBankDataSO)target;

        if (GUILayout.Button("Clear and Load Prefabs"))
        {
            foodBankDataSO.foodPrefabs.Clear();
            LoadPrefabs(foodBankDataSO);
        }
    }

    private void LoadPrefabs(FoodBankDataSO foodBankDataSO)
    {
        if (string.IsNullOrEmpty(foodBankDataSO.folderPath))
        {
            Debug.LogWarning("Folder path is empty.");
            return;
        }

        var guids = AssetDatabase.FindAssets("t:GameObject", new[] { foodBankDataSO.folderPath });
        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefab != null)
            {
                foodBankDataSO.foodPrefabs.Add(prefab);
            }
        }
        EditorUtility.SetDirty(foodBankDataSO);
    }
}
