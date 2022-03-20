using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Words Theme", fileName = "New Words Theme")]
public class Words : ScriptableObject
{
    public List<string> WordsList;
    public string ThemeName;
    public int WordsGuessedNum;

    public bool CurrentlyPlaying;
    public bool ThemeDeactivated;
    public List<string> ShuffleMyList()
    {
        WordsList.ListShuffle();
        return WordsList;
    }
}
