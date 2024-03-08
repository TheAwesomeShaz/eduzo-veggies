using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public event Action OnModeSelectComplete;
    public event Action<string> OnVegetableSelectComplete;
    public event Action<GameMode,VegetableType,Sprite> OnSpeakComplete;

    public event Action<string> OnScoreUpdated;
    public event Action<string> OnLivesUpdated;
    public event Action<int,string> OnCatchVeggieGameComplete;

    [SerializeField] private int scoreIncrement = 10;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float catchVeggieGameTime = 60f;

    [SerializeField] private VeggieSelectController veggieSelectController;
    [SerializeField] private SpeakController speakController;
    [SerializeField] private ModeSelectController modeSelectController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private FloorController floorController;
    [SerializeField] private EndScreenController endScreenController;

    private GameMode currentGameMode;
    private VegetableType currentVegetable;
    private string currentVegetableName;
    private Sprite currentVegetableSprite;

    private int mScore;
    private int mLives;

    private void Start()
    {
        mScore = 0;
        mLives = maxLives;

        modeSelectController.OnModeSelectCompleted += ModeSelectController_OnModeSelectCompleted;
        veggieSelectController.OnSelectVegetableCompleted += VeggieSelectController_OnSelectVegetableCompleted;
        speakController.OnSpeakCompleted += SpeakController_OnSpeakCompleted;

        playerController.OnCatchCorrectVegetable += PlayerController_OnCatchCorrectVegetable;
        playerController.OnCatchWrongVegetable += PlayerController_OnCatchWrongVegetable;
        floorController.OnCorrectVegetableMissed += FloorController_OnCorrectVegetableMissed;

        endScreenController.OnGameEnded += EndScreenController_OnGameEnded;
    }

    private void EndScreenController_OnGameEnded()
    {
        ReloadScene();
    }

    private static void ReloadScene()
    {
        // Reload the GameScene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FloorController_OnCorrectVegetableMissed()
    {
        ReduceLife();
    }

    private void PlayerController_OnCatchWrongVegetable()
    {
        ReduceLife();
    }

    private void PlayerController_OnCatchCorrectVegetable()
    {
        IncreaseScore();
    }

    private void IncreaseScore()
    {
        mScore += scoreIncrement;
        OnScoreUpdated?.Invoke(mScore.ToString());
    }

    private void ReduceLife()
    {
        mLives--;
        if(mLives <= 0)
        {
            EnableEndGameScreenWithScoreAndStars();
        }
        else
        {
            OnLivesUpdated?.Invoke(mLives.ToString());
        }
    }

    private void ModeSelectController_OnModeSelectCompleted(GameMode gameMode)
    {
        currentGameMode = gameMode;
        OnModeSelectComplete?.Invoke();
    }
    private void VeggieSelectController_OnSelectVegetableCompleted(VegetableDataSO.VegetableData vegetable)
    {
        currentVegetable = vegetable.type;
        currentVegetableName = vegetable.name;
        currentVegetableSprite = vegetable.sprite;

        switch (currentGameMode)
        {
            case GameMode.Learn:
                OnVegetableSelectComplete?.Invoke(currentVegetableName);
                break;
            case GameMode.Test:
                StartCatchVeggieGame();
                break;
            default:
                break;
        }

    }

    private void SpeakController_OnSpeakCompleted()
    {
        StartCatchVeggieGame();
    }

    private void StartCatchVeggieGame()
    {
        OnSpeakComplete?.Invoke(currentGameMode, currentVegetable, currentVegetableSprite);
        OnLivesUpdated?.Invoke(mLives.ToString());
        OnScoreUpdated?.Invoke(mScore.ToString());
        StartCoroutine(WaitForTimeAndEndGameCoR(catchVeggieGameTime));
    }

    private IEnumerator WaitForTimeAndEndGameCoR(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableEndGameScreenWithScoreAndStars();
    }

    private void EnableEndGameScreenWithScoreAndStars()
    {
        Debug.Log("END GAME TRIGGERED");
        // Passing the Number of stars as an "int" and the score as a "string"
        switch (mScore)
        {
            case > 90:
                OnCatchVeggieGameComplete?.Invoke(3, mScore.ToString());
                break;
            case < 100 and > 50:
                OnCatchVeggieGameComplete?.Invoke(2, mScore.ToString());
                break;
            case > 0:
                OnCatchVeggieGameComplete?.Invoke(1, mScore.ToString());
                break;
            default:
                OnCatchVeggieGameComplete?.Invoke(0, mScore.ToString());
                break;
        }
    }
}

// Separate them into separate files later
public enum GameMode
{
    Learn,
    Test,
}

