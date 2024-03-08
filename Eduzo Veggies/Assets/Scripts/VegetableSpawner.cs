using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VegetableSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private CatchVeggieController catchVeggieController;
    [SerializeField] private VegetableDataSO vegetableDataSO;
    [SerializeField] private GameObject vegetableImagePrefab;

    private Sprite mCurrentVegetableSprite;
    private SpawnMode mCurrentSpawnMode;
    private VegetableType mCurrentVegetableType;
    private int mVegetableDataLength;

    private void Awake()
    {
        mVegetableDataLength = vegetableDataSO.vegetableDataList.Count;

        catchVeggieController.OnSetSpawnMode += CatchVeggieController_OnSetSpawnMode;
        catchVeggieController.OnSetVegetableType += CatchVeggieController_OnSetVegetableType;
        catchVeggieController.OnSetSpawnerSprite += CatchVeggieController_OnSetSpawnerSprite;
        catchVeggieController.OnStartSpawning += CatchVeggieController_OnStartSpawning;
    }

    private void CatchVeggieController_OnSetVegetableType(VegetableType vegetableType)
    {
        mCurrentVegetableType = vegetableType;
    }

    private void CatchVeggieController_OnStartSpawning()
    {
        switch (mCurrentSpawnMode)
        {
            case SpawnMode.Single:
                StartCoroutine(SpawnSameSpriteAfterDelayCoR(spawnInterval));
                break;
            case SpawnMode.Random:
                StartCoroutine(SpawnRandomSpriteAfterDelayCoR(spawnInterval));
                break;
        }
    }

    private void CatchVeggieController_OnSetSpawnerSprite(Sprite sprite)
    {
        mCurrentVegetableSprite = sprite;
    }

    private void CatchVeggieController_OnSetSpawnMode(SpawnMode spawnMode)
    {
        mCurrentSpawnMode = spawnMode;
    }

    private IEnumerator SpawnRandomSpriteAfterDelayCoR(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            GameObject vegetable = Instantiate(vegetableImagePrefab,this.transform);
            int randomIndex = Random.Range(0, mVegetableDataLength);
            var currentVegetableData = vegetableDataSO.vegetableDataList[randomIndex];
            vegetable.GetComponent<VegetableController>().InitVegetable(currentVegetableData.sprite, currentVegetableData.type);
            Destroy(vegetable, 10f);
        }
    }

    private IEnumerator SpawnSameSpriteAfterDelayCoR(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            GameObject vegetable = Instantiate(vegetableImagePrefab, this.transform);
            vegetable.GetComponent<VegetableController>().InitVegetable(mCurrentVegetableSprite,mCurrentVegetableType);
            Destroy(vegetable, 10f);
        }
    }
}

// Separate enum into a separate file later
public enum SpawnMode
{
    Single,
    Random,
}