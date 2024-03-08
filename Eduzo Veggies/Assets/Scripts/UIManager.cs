using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public event Action<string> OnUpdateSpeakUI;
    public event Action<GameMode,VegetableType,Sprite> OnUpdateCatchVeggieUI;
    public event Action<int, string> OnUpdateEndGameScreenUI;


    public event Action<string> OnUpdateScoreUI;
    public event Action<string> OnUpdateLivesUI;
    


    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject modeSelectUI;
    [SerializeField] private GameObject vegetableSelectUI;
    [SerializeField] private GameObject speakUI;
    [SerializeField] private GameObject catchVeggieUI;
    [SerializeField] private GameObject endGameScreenUI;
    
    private void Awake()
    {
        gameManager.OnVegetableSelectComplete += GameManager_OnVegetableSelectComplete;
        gameManager.OnModeSelectComplete += GameManager_OnModeSelectComplete;
        gameManager.OnSpeakComplete += GameManager_OnSpeakComplete;

        gameManager.OnCatchVeggieGameComplete += GameManager_OnCatchVeggieGameComplete;
        gameManager.OnScoreUpdated += GameManager_OnScoreUpdated;
        gameManager.OnLivesUpdated += GameManager_OnLivesUpdated;

        ResetUI();
    }

    private void GameManager_OnModeSelectComplete()
    {
        DisableAllUI();
        vegetableSelectUI.SetActive(true);
    }
    private void GameManager_OnVegetableSelectComplete(string vegetableName)
    {
        DisableAllUI();
        speakUI.SetActive(true);

        OnUpdateSpeakUI?.Invoke(vegetableName);
    }

    private void GameManager_OnSpeakComplete(GameMode gameMode, VegetableType type, Sprite sprite)
    {
        DisableAllUI();
        catchVeggieUI.SetActive(true);

        OnUpdateCatchVeggieUI?.Invoke(gameMode, type, sprite);
    }

    private void GameManager_OnLivesUpdated(string lives)
    {
        OnUpdateLivesUI?.Invoke(lives);
    }

    private void GameManager_OnScoreUpdated(string score)
    {
        OnUpdateScoreUI?.Invoke(score);
    }

    private void GameManager_OnCatchVeggieGameComplete(int stars, string score)
    {
        DisableAllUI();
        endGameScreenUI.SetActive(true);

        OnUpdateEndGameScreenUI?.Invoke(stars, score);
    }

    private void DisableAllUI()
    {
        modeSelectUI.SetActive(false);
        vegetableSelectUI.SetActive(false);
        speakUI.SetActive(false);
        catchVeggieUI.SetActive(false);
        endGameScreenUI.SetActive(false);   
    }

    private void ResetUI()
    {
        DisableAllUI();
        modeSelectUI.SetActive(true);
    }
}
