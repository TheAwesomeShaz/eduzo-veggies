using UnityEngine;
using UnityEngine.UI;

public class VegetableController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float moveSpeed; 
    [SerializeField] private bool isImageSet;
    
    private VegetableType mVegetableType;

    void Update()
    {
        if (isImageSet)
        {
            image.rectTransform.anchoredPosition += Vector2.down * moveSpeed * Time.deltaTime;
        }
    }
    public void InitVegetable(Sprite sprite, VegetableType type)
    {
        image.sprite = sprite;
        mVegetableType = type;
        image.SetNativeSize();
        // All images are of different sizes doesnt feel normal
        image.rectTransform.localScale = Vector2.one * 0.4f;
        isImageSet = true;
    }


    public VegetableType GetVegetableType()
    {
        return mVegetableType;
    }

    public void DestroySelf(VegetableDestroyMode destroyMode = VegetableDestroyMode.Failure)
    {
        switch (destroyMode)
        {
            // Respective Particle Effect here?

            case VegetableDestroyMode.Success:
                break;
            case VegetableDestroyMode.Failure:
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}

public enum VegetableDestroyMode
{
    Success,
    Failure,
}