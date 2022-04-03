using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class KeyBoard : MonoBehaviour
{
    [Header("Letter Lists")]
    private List<string> _allLetters = new List<string>();
    private List<string> _notUsedLetters = new List<string>();
    private List<string> _lettersNeeded = new List<string>();
    private List<string> _lettersToDisplay = new List<string>();

    private List<GameObject> _activeButtons = new List<GameObject>();
    private List<ButtonScrt> _buttonScripts = new List<ButtonScrt>();

    [Header("Word Lenght Checkers")]
    [SerializeField] private int wordLenghtMax;
    [Range(0,26)]
    [SerializeField] private int buttonsLenghtMinValue;
    [Range(6, 26)]
    [SerializeField] private int buttonsLenghtMaxValue;

    [Header("Scripts")]
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private ObjectPool objectpool;


    private void Start()
    {

    }
    private int wordLenght()
    {
        if (_lettersToDisplay.Count > wordLenghtMax)
        {
            return buttonsLenghtMaxValue;
        }
        else return buttonsLenghtMinValue;

    }

    public void SpawnNewBoard()
    {
        ClearAll();
        string word = playerInfo.GetCurrentWord();
        for (int i = 0; i < _allLetters.Count; i++)
        {
            if (word.Contains(_allLetters[i])) _lettersToDisplay.Add(_allLetters[i]);
            else _notUsedLetters.Add(_allLetters[i]);
        }

        ShuffleThisList(_notUsedLetters);
        
        CreateListOfLetters();
    }

    public void CreateListOfLetters()
    {
        wordLenght();
        InsertCorrectLettersToList(wordLenght() - _lettersToDisplay.Count);

        ShuffleThisList(_lettersToDisplay);

        InsertButtonInfo();

    }

    private void InsertCorrectLettersToList(int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            _lettersToDisplay.Add(_notUsedLetters[i]);
        }
    }


    public void InsertButtonInfo()
    {

        if (_activeButtons.Count > 0)
        {
            for (int i = 0; i < _activeButtons.Count; i++)
            {

                if (_activeButtons.Count > _lettersToDisplay.Count)
                {
                    _activeButtons.RemoveAt(0);
                    objectpool.DeactivatePoolObj("button");
                }
            }
        }
        for (int k = 0; k < _lettersToDisplay.Count; k++)
        {
            if (_activeButtons.Count < _lettersToDisplay.Count)
            {
                _activeButtons.Add(objectpool.GetKeyButton("button"));
            }
            _activeButtons[k].GetComponent<ButtonScrt>().InsertLetterInKey(_lettersToDisplay[k]);
        }
        
    }

    public void ClearAll()
    {
        _lettersNeeded.Clear();
        _lettersToDisplay.Clear();
        _allLetters.Clear();
        _notUsedLetters.Clear();
        foreach (GameObject button in _activeButtons)
        {
            button.GetComponent<ButtonScrt>().ClearValues();
        }
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            _allLetters.Add(letter.ToString());
        }
        
    }
    public void ActivateAllButtons()
    {
        for (int i = 0; i < _activeButtons.Count; i++)
        {
            _activeButtons[i].GetComponent<ButtonScrt>().ActivateButton();
        }
    }
    public void DeactivateAllButtons()
    {
        for (int i = 0; i < _activeButtons.Count; i++)
        {
            _activeButtons[i].GetComponent<ButtonScrt>().DeactivateButton();
        }
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
