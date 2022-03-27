using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour

{
    [SerializeField] private Transform lettersParentTransform;
    [SerializeField] private List<Words> themeWords;
    [SerializeField] private GameObject emptyObj;

    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SetLetter letterObj;
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private ChooseTheme chooseThemeScrpt;
    [SerializeField] private WordInfo wordInfo;
    [SerializeField] private Themes themes;

    public int WordCount { get; set; }
    public bool hintAllowed { get; private set; } //relace from "hints this round
    public static string currentWord;
    private char[] wordLettersChars;
    private int _correctLetterValue;
    private int _neededCorrectCount;
    private int _lastThemeListNum;

    private List<string> _wordsList;
    private List<string> _unusedWords;


    private int rightLetters;
    private int wrongLetters;

    private void Start()
    {
        hintAllowed = true;
        WordCount = 0;
    }
    public void ChooseTheme()
    {
        int themeNum = playerInfo.ThemeNum;
        _lastThemeListNum = themeNum;
        if (!gameManager.gameStarted)
        {
            _wordsList = themes.WordThemes[themeNum].ShuffleMyList();
        }
        else
        {
            SetNextWord();
            SelectNewWord();
            gameManager.StartPlaying();
        }
    }
    public void StringToChar()
    {
        wordLettersChars = currentWord.ToCharArray();
    }
    private void SpawnLetters()
    {
        for (int i = 0; i < wordLettersChars.Length; i++)
        {
            if (wordLettersChars[i].ToString() == " ")
            {
                Instantiate(emptyObj, lettersParentTransform);
            }
            else
            {
                Instantiate(letterObj, lettersParentTransform);
            }
        }
    }

    public void GetButtonInfo(Button button)
    {
        playerInfo.HasStartedGuessing = true;
        soundManager.PlaySound(Random.Range(0, 2));
        string buttonLetter = button.GetComponentInChildren<Text>().text;
        ButtonScrt thisButton = button.GetComponent<ButtonScrt>();
        if (currentWord.Contains(buttonLetter))
        {
            for (int i = 0; i < wordLettersChars.Length; i++)
            {
                if (wordLettersChars[i].ToString() == buttonLetter)
                {
                    SetCorrectLetter(i);
                    thisButton.DrawGreenLine();
                }
            }
        }
        else
        {
            WrongLetter();
            thisButton.DrawRedLine();
        }
        int timesplayedVaule = WordCount + 1;
        if (_correctLetterValue >= _neededCorrectCount && timesplayedVaule >= _wordsList.Count)
        {
            gameManager.ThemeFinished();
            themes.WordThemes[playerInfo.ThemeNum].ThemeDeactivated = true;//
            ChoosingAnotherTheme();
        }
        if (_correctLetterValue >= _neededCorrectCount && WordCount < _wordsList.Count)
        {
            gameManager.PlayAgainPanel();
        }

    }
    private void CalculateCorrectLetterValue()
    {
        for (int i = 0; i < wordLettersChars.Length; i++)
        {
            int currentCorrectValue = _neededCorrectCount;
            if (wordLettersChars[i].ToString() == " ")
            {
                _neededCorrectCount = currentCorrectValue;
            }
            else _neededCorrectCount++;
        }
    }
    public void GetHintLetter(string letter, int hintNumAllowed)
    {
        playerInfo.HasStartedGuessing = true;
        if (hintNumAllowed < 1 || !hintAllowed) return;
        for (int i = 0; i < wordLettersChars.Length; i++)
        {
            if (wordLettersChars[i].ToString() == letter)
            {
                SetCorrectLetter(i);
            }
        }
        hintAllowed = false;
    }
    private void SetCorrectLetter(int letterValue)
    {
        _correctLetterValue++;
        SetLetter tempLetter = lettersParentTransform.GetChild(letterValue).GetComponent<SetLetter>();
        tempLetter.InsertLetter(wordLettersChars[letterValue].ToString());
    }
    private void WrongLetter()
    {
        gameManager.WrongLetterChecked();
    }
    public void PrepareNewWord()
    {
        currentWord = _wordsList[WordCount];
        playerInfo.Word = currentWord;
    }

    public void ClearChildren()
    {
        foreach (Transform child in lettersParentTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void ChoosingAnotherTheme()
    {
        if (themes.WordThemes[playerInfo.ThemeNum].CurrentlyPlaying) return;
        _correctLetterValue = 0;
        _neededCorrectCount = 0;
        WordCount = 0;
        hintAllowed = true;
        if (!gameManager.gameStarted)
        {
            ClearChildren();
        }
    }
    public void ShowNextWord()
    {
        WordCount++;
        _neededCorrectCount = 0;
        if (WordCount < _wordsList.Count)
        {
            currentWord = _wordsList[WordCount];
            gameManager.playAgainField.SetActive(false);
        }
        SelectNewWord();
    }

    public void ExitingCurrentGame()
    {
        ClearChildren();
        _correctLetterValue = 0;
        hintAllowed = true;
    }

    public void SetNextWord()
    {
        SaveWordInfo();
        ShowNextWord();
        if (gameManager.gameStarted) hintAllowed = false;
        else hintAllowed = true;
        playerInfo.NoMistakesThisRound = true;
        playerInfo.HasStartedGuessing = false;
    }
    public void SelectNewWord()
    {
        ChooseTheme();
       // ChooseTheme(playerInfo.ThemeNum);
        hintAllowed = true;
        _correctLetterValue = 0;
        _neededCorrectCount = 0;
        PrepareNewWord();
        StringToChar();
        CalculateCorrectLetterValue();
        ClearChildren();
        SpawnLetters();
    }

    public void RetryWhenLost()
    {
        ChoosingAnotherTheme();
       // ChooseTheme(_lastThemeListNum);
        ChooseTheme();
        PrepareNewWord();
        StringToChar();
        CalculateCorrectLetterValue();
        SpawnLetters();
    }

    private void SaveWordInfo()
    {
        themes.WordThemes[playerInfo.ThemeNum].SaveWordInfo(_wordsList[0], rightLetters, wrongLetters);
    }
}
