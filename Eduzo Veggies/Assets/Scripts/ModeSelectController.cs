using System;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectController : MonoBehaviour
{
    public event Action<GameMode> OnModeSelectCompleted;

    [SerializeField] private Button learnButton;
    [SerializeField] private Button testButton;

    private void OnEnable()
    {
        learnButton.onClick.AddListener(OnLearnButtonClicked);
        testButton.onClick.AddListener(OnTestButtonClicked);
    }

    private void OnDisable()
    {
        learnButton.onClick.RemoveAllListeners();
        testButton.onClick.RemoveAllListeners();
    }

    private void OnTestButtonClicked()
    {
        OnModeSelectCompleted?.Invoke(GameMode.Test);
    }

    private void OnLearnButtonClicked()
    {
        OnModeSelectCompleted?.Invoke(GameMode.Learn);
    }
}
