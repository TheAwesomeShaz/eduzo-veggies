using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CatchVeggieController : MonoBehaviour
{
    public event Action<SpawnMode> OnSetSpawnMode;
    public event Action<VegetableType> OnSetVegetableType;
    public event Action<Sprite> OnSetSpawnerSprite;

    public event Action OnStartSpawning;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text catchText;
    [SerializeField] private float catchTextDisappearDelay = 3f;
    private void Awake()
    {
        catchText.gameObject.SetActive(false);
        uiManager.OnUpdateCatchVeggieUI += UiManager_OnUpdateCatchVeggieUI;

        uiManager.OnUpdateLivesUI += UiManager_OnUpdateLivesUI;
        uiManager.OnUpdateScoreUI += UiManager_OnUpdateScoreUI;
    }

    private void UiManager_OnUpdateScoreUI(string score)
    {
        scoreText.text = score;
    }

    private void UiManager_OnUpdateLivesUI(string lives)
    {
        livesText.text = $"x {lives}";
    }

    private void UiManager_OnUpdateCatchVeggieUI(GameMode gameMode, VegetableType vegetableType, Sprite sprite)
    {
        switch (gameMode)
        {
            case GameMode.Learn:
                catchText.gameObject.SetActive(true);
                catchText.text = $"CATCH THE {vegetableType.ToString()}";
                StartCoroutine(DisableCatchTextAfterTime(catchTextDisappearDelay));

                OnSetSpawnMode(SpawnMode.Single);
                OnSetSpawnerSprite(sprite);
                break;
            case GameMode.Test:
                OnSetSpawnMode(SpawnMode.Random);
                break;
        }
        OnSetVegetableType?.Invoke(vegetableType);
        OnStartSpawning();
    }

    private IEnumerator DisableCatchTextAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        catchText.gameObject.SetActive(false);
    }

}
