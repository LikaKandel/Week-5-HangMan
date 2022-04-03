using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System;

public class WordsGuessedPanel : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject themeButtonPrefab;

    [Header("Display Words")]
    [SerializeField] private GameObject wordPanelsParent;
    [SerializeField] private GameObject wordPanelPrefab;
    [SerializeField] private GameObject wordInfoPrefab;
    [SerializeField] private List<GameObject> wordPanels;
    [SerializeField] private List<GameObject> wordPanelContents;

    [Header("Display Themes")]
    [SerializeField] private GameObject themePanelParent;
    

    [Header("Scripts")]
    [SerializeField] private Themes themes;

    public delegate void OnWordsSpawned(int themeNum);
    public static OnWordsSpawned OnSpawnedWords;


    [SerializeField] private List<GameObject> themeButtonsTransforms;

    [SerializeField] private List<GuessedThemeButton> _buttons;


    private int _openedWordsPanelNum;

    /* - Spawns ThemeButtons In the start
     * - Close Panel Function
     *    - if the words gameobject is active desable it and activate the buttons panel before closing
     * - if the theme has 0 guessed words: disable the button
     *    
     */
    private void Start()
    {
        OnSpawnedWords += OpenWordsList;
        InstantiateWordPanels();
        InstantiateThemeButtons();
    }

    public void DislayAllThemes()
    {
        gameObject.SetActive(true);
    }
    private void InstantiateThemeButtons()
    {
        for (int i = 0; i < themes.WordThemes.Length; i++)
        {
            var thisButton = Instantiate(themeButtonPrefab, themePanelParent.transform);
            _buttons.Add(thisButton.GetComponent<GuessedThemeButton>()); 
            _buttons[i].SendThemeInfo(i, wordPanelContents[i].transform);
        }
    }
    private void InstantiateWordPanels()
    {
        for (int i = 0; i < themes.WordThemes.Length; i++)
        {
            var wordParent = Instantiate(wordPanelPrefab, wordPanelsParent.transform);
            wordPanels.Add(wordParent);
            wordPanelContents.Add(wordParent.transform.GetChild(1).GetChild(0).gameObject);
        }
    }
    public void ToggleThisPanel()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
    private void OpenWordsList(int themenum)
    {
        _openedWordsPanelNum = themenum;
        wordPanels[themenum].SetActive(true);
        themePanelParent.SetActive(false);
    }
    public void ClosePanel()
    {
        themePanelParent.SetActive(true);
        wordPanels[_openedWordsPanelNum].SetActive(false);
        gameObject.SetActive(false);
    }
    public void ReturnToThemeList()
    {
        wordPanels[_openedWordsPanelNum].SetActive(false);
        themePanelParent.SetActive(true);
    }
}
