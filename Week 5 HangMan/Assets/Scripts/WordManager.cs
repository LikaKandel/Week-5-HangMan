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
    [SerializeField] private Themes themes;

    public int WordCount { get; set; }
    public bool HintAllowed { get; private set; } //relace from "hints this round
    public static string CurrentWord;
    private char[] _wordLettersChars;
    private int _correctLetterValue;
    private int _correctHintValue;
    private int _neededCorrectCount;
    private int _lastThemeListNum;

    private List<string> _wordsList = new List<string>();


    private int _rightLetters;
    private int _wrongLetters;

    private void Start()
    {
        HintAllowed = true;
        WordCount = 0;
        
    }
    
    public void StartGame()
    {
        SetNewWordValues();
        SpawnNextWord();
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
            _wordsList = themes.WordThemes[themeNum].ShuffleMyList();
            SpawnNextWord();
        }
    }
    //-------Word Functions--------
    public void SetNewWordValues()
    {
        int themeNum = playerInfo.ThemeNum;
        _lastThemeListNum = themeNum;
        _wordsList = themes.WordThemes[themeNum].ShuffleMyList();
        
        CurrentWord = _wordsList[0];//used to be WordCount
        playerInfo.Word = CurrentWord;
        HintAllowed = true;
        _correctLetterValue = 0;
        _neededCorrectCount = 0;
        StringToChar();
    }
    public void SpawnNextWord()
    {
        gameManager.playAgainField.SetActive(false);
        ResetWordValues();
        StringToChar();
        CalculateCorrectLetterValue();
        if (lettersParentTransform.childCount != 0)
        {
            ClearUnderlines();
        }
        SpawnLetters();
    }
    private void ResetWordValues()
    {
        HintAllowed = true;
        CurrentWord = _wordsList[0];
        playerInfo.Word = CurrentWord;
        _correctLetterValue = 0;
        _correctHintValue = 0;
        _wrongLetters = 0;
        _neededCorrectCount = 0;
    }

    private void CalculateCorrectLetterValue()
    {
        for (int i = 0; i < _wordLettersChars.Length; i++)
        {
            int currentCorrectValue = _neededCorrectCount;
            if (_wordLettersChars[i].ToString() == " ")
            {
                _neededCorrectCount = currentCorrectValue;
            }
            else _neededCorrectCount++;
        }
    }
    public void StringToChar()
    {
        _wordLettersChars = CurrentWord.ToCharArray();
    }
    public void ClearUnderlines()//replacewith pool
    {
        foreach (Transform child in lettersParentTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    private void SpawnLetters()//replacewith pool
    {
        for (int i = 0; i < _wordLettersChars.Length; i++)
        {
            if (_wordLettersChars[i].ToString() == " ")
            {
                Instantiate(emptyObj, lettersParentTransform);
            }
            else
            {
                Instantiate(letterObj, lettersParentTransform);
            }
        }
    }

    private void SaveGuessedWord()
    {
        themes.WordThemes[playerInfo.ThemeNum].SaveWordInfo(_wordsList[0], _rightLetters, _wrongLetters);
        _wordsList.RemoveAt(0);
    }
    // --------- Button Functions -----------
    public void GetButtonInfo(Button button)
    {
        playerInfo.HasStartedGuessing = true;
        soundManager.PlaySound(Random.Range(0, 2));
        string buttonLetter = button.GetComponentInChildren<Text>().text;
        ButtonScrt thisButton = button.GetComponent<ButtonScrt>();
        if (CurrentWord.Contains(buttonLetter))
        {
            for (int i = 0; i < _wordLettersChars.Length; i++)
            {
                if (_wordLettersChars[i].ToString() == buttonLetter)
                {
                    SetCorrectLetter(i);
                    thisButton.DrawGreenLine();
                }
            }
            _rightLetters++;
        }
        else
        {
            WrongLetter();
            thisButton.DrawRedLine();
        }
        int timesplayedVaule = WordCount + 1;
        if (_correctLetterValue >= _neededCorrectCount && timesplayedVaule >= _wordsList.Count)
        {
            SaveGuessedWord();
            playerInfo.HasFinishedCurrentTheme = true;
            themes.WordThemes[playerInfo.ThemeNum].ThemeDeactivated = true;
            ChoosingAnotherTheme();
        }
        if (_correctLetterValue >= _neededCorrectCount && _wordsList.Count >= 0 && timesplayedVaule < _wordsList.Count) 
        {
            CheckBonus();
            gameManager.PlayAgainPanel();
            SaveGuessedWord();
        }

    }
    private void ChoosingAnotherTheme()
    {
        gameManager.ThemeFinished();
        themes.WordThemes[playerInfo.ThemeNum].ThemeDeactivated = true;
    }
    public void GetHintLetter(string letter, int hintNumAllowed)
    {
        print("should set letter");
        playerInfo.HasStartedGuessing = true;
        if (hintNumAllowed < 1 || !HintAllowed) return;
        print("cheked if hint is allowed" + hintNumAllowed);
        for (int i = 0; i < _wordLettersChars.Length; i++)
        {
            if (_wordLettersChars[i].ToString() == letter)
            {
                SetCorrectHint(i);
            }
        }
        int timesplayedVaule = WordCount + 1;
        HintAllowed = false;
        if (_correctLetterValue >= _neededCorrectCount && _wordsList.Count >= 0 && timesplayedVaule < _wordsList.Count)
        {
            CheckBonus();
            gameManager.PlayAgainPanel();
            SaveGuessedWord();
        }
    }
    private void SetCorrectLetter(int letterValue)
    {
        _correctLetterValue++;
        SetLetter tempLetter = lettersParentTransform.GetChild(letterValue).GetComponent<SetLetter>();//create pool
        tempLetter.InsertLetter(_wordLettersChars[letterValue].ToString());
    }
    private void SetCorrectHint(int letterValue)
    {
        _correctLetterValue++;
        _correctHintValue++;
        SetLetter tempLetter = lettersParentTransform.GetChild(letterValue).GetComponent<SetLetter>();
        tempLetter.InsertLetter(_wordLettersChars[letterValue].ToString());
    }
    private void WrongLetter()
    {
        _wrongLetters++;
        playerInfo.AddBodyPart();
        gameManager.WrongLetterChecked();
    }
    //---------Game Functions----------
    private void CheckBonus()
    {
        if (_wrongLetters <= 0 && _correctHintValue <= 0)
        {
            playerInfo.RemoveManPart();
        }
    }
    public void ExitingCurrentGame()
    {
        ClearUnderlines();
        _correctLetterValue = 0;
        HintAllowed = true;
    }

    public void RetryWhenLost()
    {
        
    }

    
}
