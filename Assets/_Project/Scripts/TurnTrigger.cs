using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;

    private void OnMouseDown()
    {
        turnManager.OpenFridge();
    }
}
