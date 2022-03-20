using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public int StickManLives;
    public int WrongValue;
    public int PlayerLives;
    public bool NoMistakesThisRound;
    public int CorrectLetterAmount;
    public int WrongLetterAmount;

    public bool GameStarted;
    public bool ThemeChosen;

    public int ThemeNum;
    public bool HasStartedGuessing;
    public string Word;
    public string ThemeName;

    public int AnimalsGuessedNum;
    public int LocationsGuessedNum;
    public int RandomGuessedNum;


    public void ResetGame()
    {
        StickManLives = 0; 
        
        NoMistakesThisRound = true;
        GameStarted = false;
        ThemeChosen = false;
        PlayerLives = 3;
        WrongValue = 0;
        Word = "";
        ThemeName = "SELECT";
        HasStartedGuessing = false;

        AnimalsGuessedNum = 0;
        LocationsGuessedNum = 0;
        RandomGuessedNum = 0;
    }
    public void GiveOneMoreChance()
    {
        RemoveManPart();
        PlayerLives--;
        WrongValue--;
        NoMistakesThisRound = false;
        HasStartedGuessing = true;
    }
    public string GetCurrentWord()
    {
        return Word;
    }
    
    public void RemoveManPart()
    {
        StickManLives--;
    }
    public void AddBodyPart()
    {
        StickManLives++;
    }
    public void SetVarialbles()
    {
        LoadHighScore("HighScore");
        ResetGame();
    }
    public void AddWordGuessedNum()
    {
        switch (ThemeName)
        {
            case "ANIMALS":
                AnimalsGuessedNum++;
                break;
            case "LOCATIONS":
                LocationsGuessedNum++;
                break;
            case "RANDOM":
                RandomGuessedNum++;
                break;
            default:
                break;
        }
    }
    public void SaveScores()
    {
        PlayerPrefs.SetInt("Animals", AnimalsGuessedNum);
        PlayerPrefs.SetInt("Locations", LocationsGuessedNum);
        PlayerPrefs.SetInt("Random", RandomGuessedNum);
    }
    public void LoadHighScore(string name)
    {
        PlayerPrefs.GetInt(name);
    }
    public void ChooseThemeName(string Name)
    {
        ThemeChosen = true;
        ThemeName = Name;
    }
}
