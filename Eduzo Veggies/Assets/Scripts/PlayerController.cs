using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public event Action OnCatchCorrectVegetable;
    public event Action OnCatchWrongVegetable;

    [SerializeField] private CatchVeggieController catchVeggieController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float xLimit;

    private VegetableType mCurrentVegetableType;

    private void Awake()
    {
        rectTransform = GetComponent<Image>().rectTransform;
        catchVeggieController.OnSetVegetableType += CatchVeggieController_OnSetVegetableType;
    }

    private void CatchVeggieController_OnSetVegetableType(VegetableType vegetableType)
    {
        mCurrentVegetableType = vegetableType;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 

        if (horizontalInput > 0 || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2 newPosition = rectTransform.anchoredPosition + Vector2.right * moveSpeed;
            newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);
            rectTransform.anchoredPosition = newPosition;
        }

        else if (horizontalInput < 0 || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2 newPosition = rectTransform.anchoredPosition + Vector2.left * moveSpeed;
            newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);
            rectTransform.anchoredPosition = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.TryGetComponent(out VegetableController vegetableController))
        {
            if(mCurrentVegetableType == vegetableController.GetVegetableType())
            {
                Debug.Log("Correct Vegetable Caught "+mCurrentVegetableType);
                OnCatchCorrectVegetable?.Invoke();
            }
            else
            {
                Debug.Log("Wrong Vegetable Caught, Catch "+ mCurrentVegetableType);
                OnCatchWrongVegetable?.Invoke();
            }

            vegetableController.DestroySelf();
        }
    }

}
