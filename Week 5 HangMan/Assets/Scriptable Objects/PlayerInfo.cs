using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public int StickManLives;
    public int WrongValue;
    public int PlayerLives;
    public bool NoMistakesThisRound;

    public bool GameStarted;
    public bool ThemeChosen;
    public bool HasFinishedCurrentTheme;

    public int ThemeNum;
    public bool HasStartedGuessing;
    public string Word;
    public string ThemeName;

    public Themes Themes;
    private Words Words;

    
    public void ResetGame()
    {
        StickManLives = 0; 
        NoMistakesThisRound = true;
        GameStarted = false;
        PlayerLives = 3;
        WrongValue = 0;
        Word = "";
        ThemeName = "SELECT";
        HasStartedGuessing = false;
    }

    public void GiveOneMoreChance()
    {
        RemoveManPart();
        NoMistakesThisRound = false;
        HasStartedGuessing = true;
    }
    public string GetCurrentWord()
    {
        return Word;
    }
    public void SetCurrentTheme(int themeNum)
    {
        Words = Themes.WordThemes[themeNum];
        ThemeChosen = true;
        ThemeName = Words.ThemeName;
        ThemeNum = themeNum;
    }
    public void RemoveManPart()
    {
        if (StickManLives < 0 || StickManLives > 7) return;
        StickManLives--;
    }
    public void AddBodyPart()
    {
        if (StickManLives >= 7) return;
        StickManLives++;
        NoMistakesThisRound = false;
    }
}
