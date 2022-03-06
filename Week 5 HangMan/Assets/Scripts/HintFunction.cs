using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class HintFunction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform keyboardParnt;
    [SerializeField] private Text hintNumText;
    [SerializeField] private int startHintAmount;

    [SerializeField] private GameObject timerDisplay;
    [SerializeField] private GameObject boxTransform;
    [SerializeField] private Text timerDisplayText;

    [SerializeField] private float regenerationTimeInMinutes;
    [SerializeField] private Animator animator;

    [SerializeField] private WordManager wordManager;
    [SerializeField] private KeyBoard keyBoard;

    private string _word;
    private int currentHintAmount;
    private int lastLoopNum = 0;
    private int hintsUsedThisRound;

    private char[] wordLetters;
    private float regenerationInSeconds;
    private bool doAnimate;
    private bool mouse_over;

    private void Start()
    {
        currentHintAmount = startHintAmount;
        timerDisplay.SetActive(false);
        DisplayHintsLeft();
        doAnimate = true;
        hintsUsedThisRound = 0;
        regenerationInSeconds = regenerationTimeInMinutes * 60;
    }
    private void Update()
    {
        if (regenerationInSeconds > 0 && currentHintAmount < startHintAmount)
        {
            regenerationInSeconds -= Time.deltaTime;
        }
        else if (regenerationInSeconds <= 0)
        {
            AddHintAmount();
        }

        if (mouse_over)
        {
            doAnimate = true;
            if (currentHintAmount < startHintAmount)
            {
                DisplayTimerForRegeneration(regenerationInSeconds);
            }
            else if (currentHintAmount >= startHintAmount) DisplayTimerForRegeneration(hintAmountFull: true);
        }

        if(!mouse_over && doAnimate)
        {
            StartCoroutine(FadeOut());
        }

    }
    private void AddHintAmount()
    {
        currentHintAmount++;
    }
    private void DisplayTimerForRegeneration(float? time = null, bool? hintAmountFull = null)
    {
        if (mouse_over) timerDisplay.SetActive(true);
        if (hintAmountFull == true)
        {
            timerDisplayText.text = "FULL";
            return;
        }
        float minutes = (int)(time / 60);
        float seconds = (int)(time % 60);
        timerDisplayText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    private IEnumerator FadeOut()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(0.4f);//dueartion of the "fade out" animation
        doAnimate = false;
        animator.enabled = false;
        timerDisplay.SetActive(false);
        RestoreUITrancparancy();
    }
    public void GetHint()
    {
        TakeCurrentWord();
        SetFirstAvailableLetter();
        DisplayHintsLeft();
    }
    private void TakeCurrentWord()
    {
        _word = WordManager.currentWord;
        wordLetters = _word.ToCharArray();
    }
    
    private void SetFirstAvailableLetter()
    {
        int wordArrayNum = 0;
        for (int i = lastLoopNum; i < keyboardParnt.childCount; i++)
        {
            lastLoopNum++;
            ButtonScrt key = keyboardParnt.GetChild(i).gameObject.GetComponent<ButtonScrt>();
            if (key.thisLetter == wordLetters[wordArrayNum].ToString() && wordManager.hintAllowed && !key.isUsed)
            {
                key.DrawYellowLine();
                wordManager.GetHintLetter(key.thisLetter, currentHintAmount);
                currentHintAmount--;
                break;
            }
            else if (key.isUsed) wordArrayNum++;
        }
        lastLoopNum = 0;
    }
    public void ResetHints()
    {
        currentHintAmount = startHintAmount;
        DisplayHintsLeft();
    }

    private void DisplayHintsLeft()
    {
        hintNumText.text = "HINT : " + currentHintAmount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
    private void RestoreUITrancparancy()
    {
        Color color = new Color(1, 1, 1, 1);
        boxTransform.GetComponent<Image>().color = color;
        timerDisplayText.color = color;
    }
}
