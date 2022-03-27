using UnityEngine;
using UnityEngine.UI;

public class ThemeButton : MonoBehaviour
{
    [SerializeField] private Text themeNameText;

    [SerializeField] private Text guessedWordsText;

    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Themes themes;

    private Button thisButton;
    public string _themeName { get; private set; }
    private int _themeArrNum;
    private int _themeWordMaxCount;

    private Words thisWord;
    private void Update()
    {
        if (playerInfo.ThemeName == thisWord.ThemeName) DeactivateButton();
        else ActivateButton();
    }
    private void OnEnable()
    {
        thisButton = gameObject.GetComponent<Button>();
    }
    public void ChooseTheme() // onclick
    {
        if (thisWord.ThemeDeactivated) thisButton.enabled = false;
        if (playerInfo.ThemeName != thisWord.ThemeName)
        {
            playerInfo.ThemeNum = _themeArrNum;
            playerInfo.ChooseThemeName(thisWord.ThemeName);
            thisWord.CurrentlyPlaying = false;
        }
        else thisWord.CurrentlyPlaying = true;
    }
    private void DeactivateButton()
    {
        thisButton.enabled = false;
        thisWord.ThemeDeactivated = true;
    }
    private void ActivateButton()
    {
        thisButton.enabled = true;
        thisWord.ThemeDeactivated = false;
    }
    public void AddButtonInfo(int themeArrNum)
    {
        _themeArrNum = themeArrNum;
        print(_themeArrNum);
        thisWord = themes.WordThemes[themeArrNum];
        themeNameText.text = thisWord.ThemeName;
        _themeWordMaxCount = thisWord.WordsList.Count;
        GetThemeSize(_themeWordMaxCount);
        //thisButton = gameObject.GetComponent<Button>();
        print("finished script");
    }
    
    private void GetThemeSize(int wordMaxCount)
    {
        guessedWordsText.text = themes.WordThemes[_themeArrNum].WordsGuessedNum + " / " + wordMaxCount;
        
    }
}
