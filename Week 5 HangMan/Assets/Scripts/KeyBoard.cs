using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class KeyBoard : MonoBehaviour
{
    private List<string> _allLetters = new List<string>();
    private List<string> _notUsedLetters = new List<string>();
    private List<string> _lettersNeeded = new List<string>();
    private List<string> _lettersToDisplay = new List<string>();

    [SerializeField] private ButtonScrt buttObj;

    [Header("word lenght checkers")]
    [SerializeField] private int wordLenghtMax;
    [SerializeField] private int buttonsLenghtMinValue;
    [SerializeField] private int buttonsLenghtMaxValue;

    [SerializeField] private PlayerInfo playerInfo;

    
    private int wordLenght()
    {
        if (_lettersToDisplay.Count > wordLenghtMax)
        {
            return buttonsLenghtMaxValue;
        }
        else return buttonsLenghtMinValue;
    }


    public void ChooseLettersNeeded(string chosenWord)
    {
        ClearAll();

        for (int i = 0; i < _allLetters.Count; i++)
        {
            if (chosenWord.Contains(_allLetters[i])) _lettersToDisplay.Add(_allLetters[i]);
            else _notUsedLetters.Add(_allLetters[i]);
        }

        ShuffleThisList(_notUsedLetters);
        CreateListOfLetters();

    }

    public void CreateListOfLetters()
    {
        InsertCorrectLettersToList(wordLenght() - _lettersToDisplay.Count);

        ShuffleThisList(_lettersToDisplay);

        SpawnBoard();

    }

    private void InsertCorrectLettersToList(int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            _lettersToDisplay.Add(_notUsedLetters[i]);
        }
    }


    public void SpawnBoard()
    {
        for(int i = 0; i < _lettersToDisplay.Count; i++)
        {
            ButtonScrt k  = Instantiate(buttObj, gameObject.transform);
            k.InsertLetterInKey(_lettersToDisplay[i]);
        }
    }

    public void ClearAll()
    {
        _lettersToDisplay.Clear();
        _lettersNeeded.Clear();
        _notUsedLetters.Clear();
        _allLetters.Clear();
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            _allLetters.Add(letter.ToString());
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void PrepareForNewWord()
    {
        ClearAll();
        ChooseLettersNeeded(playerInfo.GetCurrentWord());
    }   

    private void ShuffleThisList(List<string> answ)
    {
        for (int t = 0; t < answ.Count; t++)
        {
            string tmp = answ[t];
            int r = UnityEngine.Random.Range(t, answ.Count);
            answ[t] = answ[r];
            answ[r] = tmp;
        }
    }
}
