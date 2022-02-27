using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour

{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SetLetter letterObj;
    [SerializeField] private Transform lettersParentTransform;
    //[SerializeField] private Transform buttonParentTransform;
    //[SerializeField] private KeyBoard key;
    [SerializeField] private Words[] themeWords;
    [SerializeField] private GameObject emptyObj;
    [SerializeField] private PlayerInfo playerInfo;

    private char[] wordLettersChars;
    private int correctLetterValue;
    private int neededCorrectCount;
    public static string currentWord { get; private set; }
    public int wordCount { get; set; }

    private string[] wordsArray;
    
    private int lastChosenThemeNum;
    public int hintsThisRound { get; private set; }

    private void Awake()
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
            print("game was playing");
        }
    }
    public void StringToChar()//done
    {
        wordLettersChars = currentWord.ToCharArray();
    }
    private void SpawnLetters() //done
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
    
    public void GetButtonLetter(Button button) //done
    {
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
        if (correctLetterValue >= neededCorrectCount && timesplayedVaule >= wordsArray.Length)
        {
            gameManager.ThemeFinished();
            ChoosingAnotherTheme();
        }
        if (correctLetterValue >= neededCorrectCount && wordCount < wordsArray.Length)
        {
            gameManager.PlayAgainPanel();
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
    private void SetCorrectLetter(int letterValue)//works -- send letter to underline
    {
        correctLetterValue++;
        SetLetter tempLetter = lettersParentTransform.GetChild(letterValue).GetComponent<SetLetter>();
        tempLetter.InsertLetter(wordLettersChars[letterValue].ToString());
    }
    private void WrongLetter()
    {
        gameManager.WrongLetterChacked();
    }
    public void PrepareNewWord()//done
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
        if (wordCount < wordsArray.Length)
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
        print("new word selecting");
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
