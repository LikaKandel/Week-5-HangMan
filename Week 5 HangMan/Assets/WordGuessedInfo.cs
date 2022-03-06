using UnityEngine;
using UnityEngine.UI;

public class WordGuessedInfo : MonoBehaviour
{
    [SerializeField] private Transform animalInfoParent;
    [SerializeField] private Transform locationInfoParent;
    [SerializeField] private Transform randomInfoParent;

    [SerializeField] private GameObject wordInfoObj;
    [SerializeField] private GameObject wordsGuessedPanel;

    [SerializeField] private WordInfo wordInfoScrpt;

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
