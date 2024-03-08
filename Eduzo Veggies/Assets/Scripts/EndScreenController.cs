using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    public event Action OnGameEnded;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private GameObject[] stars;

    private void OnEnable()
    {
        playAgainButton.onClick.AddListener(OnClickPlayAgain);
    }


    private void OnDisable()
    {
        playAgainButton.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        DisableAllStars();
        uiManager.OnUpdateEndGameScreenUI += UiManager_OnUpdateEndGameScreenUI;   
    }
    private void OnClickPlayAgain()
    {
        OnGameEnded?.Invoke();
    }

    private void UiManager_OnUpdateEndGameScreenUI(int activeStars, string score)
    {
        if (stars != null)
        {
            DisableAllStars();

            for (int i = 0; i < activeStars; i++)
            {
                stars[i].SetActive(true);
            }

            scoreText.text = $"SCORE : {score}/100";
        }
    }

    public void DisableAllStars()
    {
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }
    }


}
