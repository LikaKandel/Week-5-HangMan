using UnityEngine;
using UnityEngine.UI;

public class ChooseTheme : MonoBehaviour
{
    [SerializeField] private WordManager wordManager;
    [SerializeField] private PlayerInfo playerInfo;
    public int themeNum;

    [SerializeField] private Text animalGuessedNum;
    [SerializeField] private Text locationGuessedNum;
    [SerializeField] private Text randomGuessedNum;

    [SerializeField] private Words[] themeArray;

    private void OnEnable()
    {
        GetThemeSize();
    }
    private int animalWordsMax;
    private int locationWordsMax;
    private int randomWordsMax;

    public bool ThemeCurrentlyPlaying { get; private set; }
    public void ChooseAnimals()
    {
        if (playerInfo.ThemeName != "ANIMALS")
        {
            wordManager.ChooseTheme(0);
            playerInfo.ChooseThemeName("ANIMALS");
            ThemeCurrentlyPlaying = false;
        }
        else ThemeCurrentlyPlaying = true;
    }
    public void ChooseLocations()
    {
        if (playerInfo.ThemeName != "LOCATIONS")
        {
            wordManager.ChooseTheme(1);
            playerInfo.ChooseThemeName("LOCATIONS");
            ThemeCurrentlyPlaying = false;
        }
        else ThemeCurrentlyPlaying = true;
    }
    public void ChooseRandom()
    {
        if (playerInfo.ThemeName != "RANDOM")
        {
            wordManager.ChooseTheme(2);
            playerInfo.ChooseThemeName("RANDOM");
            ThemeCurrentlyPlaying = false;

        }
        else ThemeCurrentlyPlaying = true;
    }
    private void GetThemeSize()
    {
        animalWordsMax = themeArray[0].words.Length;
        locationWordsMax = themeArray[1].words.Length;
        //randomWordsMax = themeArray[2].words.Length;
        animalGuessedNum.text = playerInfo.AnimalsGuessedNum + " / " + animalWordsMax;
        locationGuessedNum.text = playerInfo.LocationsGuessedNum + " / " + locationWordsMax;
        randomGuessedNum.text = playerInfo.RandomGuessedNum + " / " + randomWordsMax;
    }
}
