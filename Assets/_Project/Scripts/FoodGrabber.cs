using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodGrabber : MonoBehaviour
{
    [Header("Grab Settings")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private float destroyDistance = 0.5f;
    private List<Food> selectedFoods = new List<Food>();

    [Header("Floating Text")]
    [SerializeField] private GameObject fadeTextGroupPrefab;

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
            SpawnFadeTextAtMousePosition(food);
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

    private void SpawnFadeTextAtMousePosition(Food food)
    {
        GameObject fadeTextGroup = Instantiate(fadeTextGroupPrefab);
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, canvas.worldCamera, out localPoint);

        fadeTextGroup.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = fadeTextGroup.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = localPoint;

        Animator[] fadeTextGroupAnimators = fadeTextGroup.GetComponentsInChildren<Animator>();
        TextMeshProUGUI[] fadeTextGroupText = fadeTextGroup.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (Animator fadeTextAnimator in fadeTextGroupAnimators)
        {
            fadeTextAnimator.Play("A_FadeText");
        }

        for (int i = 0; i < fadeTextGroupText.Length; i++)
        {
            if (i == 0) fadeTextGroupText[i].text = food.stats.hungerDelta.ToString();
            if (i == 1) fadeTextGroupText[i].text = food.stats.thirstDelta.ToString();
            if (i == 2) fadeTextGroupText[i].text = food.stats.healthDelta.ToString();
        }

        StartCoroutine(DestroyFadeText(fadeTextGroup));
    }

    private IEnumerator DestroyFadeText(GameObject textGroup)
    {
        yield return new WaitForSeconds(2f);
        Destroy(textGroup);
    }



}
