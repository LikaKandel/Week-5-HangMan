using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GuessedThemeButton : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    [SerializeField] private Text wordSizeInfo;

    [SerializeField] private Themes themes;
    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private GameObject wordInfoPrefab;
    [SerializeField] private int num;

    private Transform _wordPanelContent;
    private Words _themeWord;
    private int _themeNum;

    private Button _thisButton;
    private void Awake()
    {
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(SpawnThemeWords);
    }
    private void OnEnable()
    {
        UpdateButtonInfo();
    }
    public void SendThemeInfo(int themeNum, Transform wordPanelContent)
    {
        _themeNum = themeNum;
        _wordPanelContent = wordPanelContent;
        _themeWord = themes.WordThemes[themeNum];
        buttonText.text = $"{_themeWord.ThemeName}";
        wordSizeInfo.text = $"{ _themeWord.SavedWords.Count}/{_themeWord.StartWordSize}";

        if (_themeWord.SavedWords.Count <= 0) _thisButton.interactable = false;
        else _thisButton.interactable = true;
    }
    private void UpdateButtonInfo()
    {
        if (_themeWord == null) return;
        wordSizeInfo.text = $"{ _themeWord.SavedWords.Count}/{_themeWord.StartWordSize}";

        if (_themeWord.SavedWords.Count <= 0) _thisButton.interactable = false;
        else _thisButton.interactable = true;
    }
    public void SpawnThemeWords()
    {
        WordsGuessedPanel.OnSpawnedWords(_themeNum);
        for (int i = 0; 0 < _themeWord.SavedWords.Count +1; i++)
        {
            if (_themeWord.SavedWords[i] != null)
            {
                var wordInfo = Instantiate(wordInfoPrefab, _wordPanelContent);
                wordInfo.GetComponent<DisplayWordInfo>().DisplayInfo(_themeWord.SavedWords[i], _themeWord.RightLetters[i], _themeWord.WrongLetters[i]);
                num = i;
            }
        }
    }

}