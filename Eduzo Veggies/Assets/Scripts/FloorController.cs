using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public event Action OnCorrectVegetableMissed;

    [SerializeField] private CatchVeggieController catchVeggieController;
    private VegetableType mCurrentVegetableType;

    private void Awake()
    {
        catchVeggieController.OnSetVegetableType += CatchVeggieController_OnSetVegetableType;
    }

    private void CatchVeggieController_OnSetVegetableType(VegetableType vegetableType)
    {
        mCurrentVegetableType = vegetableType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out VegetableController vegetableController))
        {
            vegetableController.DestroySelf();
            if(mCurrentVegetableType == vegetableController.GetVegetableType())
            {
                Debug.Log("Correct Vegetable Dropped on Floor!");
                OnCorrectVegetableMissed?.Invoke();
            }
        }   
    }
}
