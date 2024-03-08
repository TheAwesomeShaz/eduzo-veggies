using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeggieSelectController : MonoBehaviour
{
    public event Action<VegetableDataSO.VegetableData> OnSelectVegetableCompleted;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private VegetableDataSO vegetableDataSO;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private Image image;

    private List<VegetableType> mVegetableTypes = new();
    private Button mImageButton;
    private RectTransform mImageTransform;
    private int mCurrentIndex;


    private void Start()
    {
        mImageTransform = image.GetComponent<RectTransform>();
        foreach (VegetableDataSO.VegetableData vegetableData in vegetableDataSO.vegetableDataList)
        {
            mVegetableTypes.Add(vegetableData.type);
        }
        mImageButton = image.GetComponent<Button>();

        UpdateImage();
        leftArrow.onClick.AddListener(OnLeftArrowClicked);
        rightArrow.onClick.AddListener(OnRightArrowClicked);
        mImageButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnSelectButtonClicked()
    {
        foreach (VegetableDataSO.VegetableData vegetableData in vegetableDataSO.vegetableDataList)
        {
            if(mVegetableTypes[mCurrentIndex] == vegetableData.type)
            {
                OnSelectVegetableCompleted?.Invoke(vegetableData);
            }
        }
    }

    private void OnRightArrowClicked()
    {
        mCurrentIndex = (mCurrentIndex + 1) % mVegetableTypes.Count;
        UpdateImage();
    }

    private void OnLeftArrowClicked()
    {
        mCurrentIndex = (mCurrentIndex - 1 + mVegetableTypes.Count) % mVegetableTypes.Count;
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (mVegetableTypes != null && mVegetableTypes.Count> 0)
        {

            foreach (VegetableDataSO.VegetableData vegetableData in vegetableDataSO.vegetableDataList)
            {
                if (mVegetableTypes[mCurrentIndex] == vegetableData.type)
                {
                    image.sprite = vegetableData.sprite;
                    image.SetNativeSize();
                    mImageTransform.localScale = Vector3.one * 2f;
                }
            }

        }
    }

}
