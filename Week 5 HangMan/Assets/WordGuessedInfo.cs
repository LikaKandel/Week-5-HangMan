using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class WordGuessedInfo : MonoBehaviour
{
    [SerializeField] private Transform animalInfoParent;
    [SerializeField] private Transform locationInfoParent;
    [SerializeField] private Transform randomInfoParent;

    [SerializeField] private GameObject wordInfoObj;
    [SerializeField] private GameObject wordsGuessedPanel;

    [SerializeField] private WordInfo wordInfoScrpt;


    private void Start()
    {
        var games = new List<Tuple<string, Nullable<double>>>()
    {
        new Tuple<string, Nullable<double>>("Fallout 3:    $", 13.95),
        new Tuple<string, Nullable<double>>("GTA V:    $", 45.95),
        new Tuple<string, Nullable<double>>("Rocket League:    $", 19.95)
    };

        games.Add(new Tuple<string, double?>("Skyrim", 15.10));
    }
    public void DisplayWordInfo()
    {
        wordsGuessedPanel.SetActive(true);
    }
    public void InstanciateWordInfo(string name)
    {
        
    }
    public void InsatiateAnimalWords()
    {
        
        DisplayWordInfo();
        for (int i = 0; i < wordInfoScrpt.AnimalWords.Count; i++)
        {
            GameObject animalWord = (GameObject)Instantiate(wordInfoObj, animalInfoParent);
            animalWord.GetComponent<AnimalWordInfo>().DisplayInfo(wordInfoScrpt.AnimalWords[i], wordInfoScrpt.AnimalRightCount[i], wordInfoScrpt.AnimalWrongCount[i]);
        }
    }

}
