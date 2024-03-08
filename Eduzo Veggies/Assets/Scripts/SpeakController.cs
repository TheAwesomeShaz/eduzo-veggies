using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SpeakController : MonoBehaviour
{
    public event Action OnSpeakCompleted;

    [SerializeField] private UIManager uiManager;

    [SerializeField] private TMP_Text speechBubbleText;
    [SerializeField] private float messagesInterval;
    [SerializeField] private GameObject correctImage;
    [SerializeField] private GameObject micImage;

    private string mVegetableName;
    private bool mSayingComplete;

    private void Awake()
    {
        uiManager.OnUpdateSpeakUI += UiManager_OnUpdateSpeakUI;
        ResetUI();
    }


    private void ResetUI()
    {
        micImage.SetActive(false);
        correctImage.SetActive(false);
        speechBubbleText.gameObject.SetActive(true);
        mSayingComplete = false;
    }

    private void UiManager_OnUpdateSpeakUI(string veggieName)
    {
        mVegetableName = veggieName;
        StartCoroutine(SayVegetableNameAndWait(messagesInterval));
    }

    private IEnumerator SayVegetableNameAndWait(float delay)
    {
        speechBubbleText.text = $"\"SAY {mVegetableName.ToUpper()}\" ";
        yield return new WaitForSeconds(delay);
        StartCoroutine(SetSayingCompleteAfterTime(delay));
        micImage.SetActive(true);
        while (!mSayingComplete)
        {
            speechBubbleText.text = ".";
            yield return new WaitForSeconds(0.1f);
            speechBubbleText.text = "..";
            yield return new WaitForSeconds(0.1f);
            speechBubbleText.text = "...";
            yield return new WaitForSeconds(0.3f);
        }

        if (mSayingComplete)
        {
            micImage.SetActive(false);
            speechBubbleText.text = $"\"{mVegetableName.ToUpper()}\"";
            yield return new WaitForSeconds(delay);
            speechBubbleText.gameObject.SetActive(false);
            correctImage.SetActive(true);
            yield return new WaitForSeconds(delay);

            OnSpeakCompleted?.Invoke();
        }
    }

    private IEnumerator SetSayingCompleteAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        mSayingComplete = true;
    }


}
