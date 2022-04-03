using UnityEngine;
using UnityEngine.UI;

public class ThemeButton : MonoBehaviour
{
    [SerializeField] private Text themeNameText;

    [SerializeField] private Text guessedWordsText;

    [SerializeField] private GameObject spriteObj;

    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Themes themes;

    private Button thisButton;
    private ChooseTheme _chooseTheme;
    public string _themeName { get; private set; }
    private int _themeArrNum;
    private int _themeWordMaxCount;

    private Words thisWord;
   
    private void Awake()
    {
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(ChooseTheme);
        _chooseTheme = gameObject.GetComponentInParent<ChooseTheme>();
    }
    public void AddButtonInfo(int themeArrNum)
    {
        _themeArrNum = themeArrNum;
        thisWord = themes.WordThemes[themeArrNum];
        if (thisWord.ThemeDeactivated)
        {
            DeactivateButton();
        }
        themeNameText.text = thisWord.ThemeName;
        _themeWordMaxCount = thisWord.StartWordSize;
        UpdateButtonInfo(themeArrNum);
    }
    private void ChooseTheme() 
    {
        /* - if (playerinfo == tihs.name)
         *   -currently plyaing 
         *   -return;
         * -if (this themes word lenght is 0)
         *   -deactivate button
         * -else 
         *   -activate sprite
         *   -set playerinfo current theme
         *   - currently playing = false;
         * 
         */

        _chooseTheme.RemoveBoxSprites();
        if (playerInfo.ThemeName == thisWord.ThemeName)
        {
            spriteObj.SetActive(true);
            return;
        }
        else if (thisWord.WordsList.Count <= 0)
        {
            thisButton.enabled = false;
            spriteObj.SetActive(false);
        }
        else
        {
            SetTheme();
        }
        // _chooseTheme.RemoveBoxSprites();
        // if (thisWord.ThemeDeactivated)
        // {
        //     thisButton.enabled = false;
        // }
        // if (playerInfo.ThemeName != thisWord.ThemeName)
        // {
        //     spriteObj.SetActive(true);
        //     playerInfo.ThemeNum = _themeArrNum;
        //     playerInfo.ChooseThemeName(thisWord.ThemeName);
        //     thisWord.CurrentlyPlaying = false;
        // }
        // else
        // {
        //     thisWord.CurrentlyPlaying = true;
        // }
    }
    private void SetTheme()
    {
        spriteObj.SetActive(true);
        playerInfo.SetCurrentTheme(_themeArrNum);
        UpdateButtonInfo(_themeArrNum);
    }
    public void RemoveBoxSprite() 
    {
        spriteObj.SetActive(false);
        print($"{thisWord.ThemeName} sprite deactivated");
    }
    public void CheckActivity(bool active)
    {
        if (active) ActivateButton();
        else DeactivateButton();
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
    public void UpdateButtonInfo(int themenum)
    {
        if (thisWord.ThemeName == playerInfo.ThemeName)
        { 
            spriteObj.SetActive(true);
        }
        else
        {
            spriteObj.SetActive(false);
        }

        guessedWordsText.text = themes.WordThemes[themenum].SavedWords.Count + " / " + themes.WordThemes[themenum].StartWordSize;
        print($"{ themes.WordThemes[themenum].SavedWords.Count}/{themes.WordThemes[themenum].StartWordSize}");
    }
}
