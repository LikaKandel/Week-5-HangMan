using UnityEngine;
using System.Collections.Generic;
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

    public bool themeIsActive { get; private set; }
    private bool animalsDeactivated;
    private bool locationsDeactivated;
    private bool randomsDeactivated;
    private void Start()
    {
        UpdateThemeNums();
    }

    public bool ThemeCurrentlyPlaying { get; private set; }
    public void ChooseAnimals()
    {
        if (animalsDeactivated) GetComponent<Button>().enabled = false;
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
        if (locationsDeactivated) GetComponent<Button>().enabled = false;
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
        if (randomsDeactivated) GetComponent<Button>().enabled = false;
        if (playerInfo.ThemeName != "RANDOM")
        {
            wordManager.ChooseTheme(2);
            playerInfo.ChooseThemeName("RANDOM");
            ThemeCurrentlyPlaying = false;
        }
        else ThemeCurrentlyPlaying = true;
    }
    public void UpdateThemeNums()
    {
        int animalMax = themeArray[0].words.Count;
        int locationMax = themeArray[1].words.Count;
        int randomMax = themeArray[2].words.Count;

        GetThemeSize(animalSize: animalMax, locationSize: locationMax, randomSize: randomMax);
    }
    private void GetThemeSize( int? animalSize, int? locationSize,int? randomSize )
    {
        animalGuessedNum.text = playerInfo.AnimalsGuessedNum + " / " + animalSize;
        locationGuessedNum.text = playerInfo.LocationsGuessedNum + " / " + locationSize;
        randomGuessedNum.text = playerInfo.RandomGuessedNum + " / " + randomSize;
    }

    public void DeactivateTheme(string Name)
    {
        switch (Name)
        {
            case "ANIMALS":
                animalsDeactivated = true;
                break;
            case "LOCATIONS":
                locationsDeactivated = true;
                break;
            case "RANDOM":
                randomsDeactivated = true;
                break;

            default:
                break;
        }
    }
}
