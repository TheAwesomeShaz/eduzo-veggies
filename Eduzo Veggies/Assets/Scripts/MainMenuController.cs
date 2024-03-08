using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private Slider progressBar;

    private void Start()
    {
        loadingScreen.SetActive(false);
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnCLickPlayButton);
    }

    private void OnCLickPlayButton()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveAllListeners();
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            loadingText.text = $"Loading... {((int)(progress * 100f)).ToString()}%";

            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            if (asyncOperation.progress >= 0.9f)
            {
                progressBar.value = 1f;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
