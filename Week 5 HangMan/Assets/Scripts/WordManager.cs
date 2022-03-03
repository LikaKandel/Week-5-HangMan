using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WordManager : MonoBehaviour

{
    [SerializeField] private Transform lettersParentTransform;
    [SerializeField] private List<Words> themeWords;
    [SerializeField] private GameObject emptyObj;

    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SetLetter letterObj;
    [SerializeField] private SoundManager soundManager;

    private char[] wordLettersChars;
    private int correctLetterValue;
    private int neededCorrectCount;
    public static string currentWord { get; private set; }
    public int wordCount { get; set; }

    private List<string> wordsArray;
    private List<string> unusedWords;   
    
    private int lastChosenThemeNum;
    public int hintsThisRound { get; private set; }

    private void Start()
    {
        hintsThisRound = 0;
        wordCount = 0;

        
    }
    public void ChooseTheme(int themeNum)
    {
        lastChosenThemeNum = themeNum;
        wordsArray = themeWords[themeNum].ShuffleMyArray();
        if (gameManager.gameStarted)
        {
            SetNextWord();
            SelectNewWord();
            gameManager.StartGame();
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
    
    public void GetButtonLetter(Button button) 
    {
        
        soundManager.PlaySound(Random.Range(0,2));
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
        int timesplayedVaule = wordCount + 1;
        if (correctLetterValue >= neededCorrectCount && timesplayedVaule >= wordsArray.Count)
        {
            gameManager.ThemeFinished();
            ChoosingAnotherTheme();
        }
        if (correctLetterValue >= neededCorrectCount && wordCount < wordsArray.Count)
        {
            gameManager.PlayAgainPanel();
            unusedWords.Add(wordsArray[0]);
            wordsArray.RemoveAt(0);
        }
       
    }
    private void CalculateCorrectLetterValue()
    {
        for (int i = 0; i < wordLettersChars.Length; i++)
        {
            int currentCorrectValue = neededCorrectCount;
            if (wordLettersChars[i].ToString() == " ")
            {
                neededCorrectCount = currentCorrectValue;
            }
            else neededCorrectCount++;
        }
    }
    public void GetHintLetter(string letter, int hintNumAllowed)
    {
        for (int i = 0; i < wordLettersChars.Length; i++)
        {
            if (hintNumAllowed < 1 ||  hintsThisRound >= 1) 
            {
                break;
            } 
            if (wordLettersChars[i].ToString() == letter)
            {
                SetCorrectLetter(i);
            }
        }
        hintsThisRound++;
        
    }
    private void SetCorrectLetter(int letterValue)
    {
        correctLetterValue++;
        SetLetter tempLetter = lettersParentTransform.GetChild(letterValue).GetComponent<SetLetter>();
        tempLetter.InsertLetter(wordLettersChars[letterValue].ToString());
    }
    private void WrongLetter()
    {
        gameManager.WrongLetterChacked();
    }
    public void PrepareNewWord()
    {
        currentWord = wordsArray[wordCount];
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
        correctLetterValue = 0;
        neededCorrectCount = 0;
        wordCount = 0;
        hintsThisRound = 0;
        ClearChildren();
    }
    public void ShowNextWord()
    {
        wordCount++;
        neededCorrectCount = 0;
        if (wordCount < wordsArray.Count)
        {
            currentWord = wordsArray[wordCount];
            gameManager.playAgainField.SetActive(false);
        }
        SelectNewWord();
    }

    public void ExitingCurrentGame()
    {
        ClearChildren();
        correctLetterValue = 0;
        hintsThisRound = 0;
    }

    public void SetNextWord()
    {
        ClearChildren();
        ShowNextWord();
        playerInfo.NoMistakesThisRound = true;
    }
    public void SelectNewWord()
    {
        hintsThisRound = 0;
        correctLetterValue = 0;
        neededCorrectCount = 0;
        PrepareNewWord();
        StringToChar();
        CalculateCorrectLetterValue();
        ClearChildren();
        SpawnLetters();
    }

    public void RetryWhenLost()
    {
        ChoosingAnotherTheme();
        ChooseTheme(lastChosenThemeNum);
        PrepareNewWord();
        StringToChar();
        CalculateCorrectLetterValue();
        SpawnLetters();
    }
}
