using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Words Theme", fileName = "New Words Theme")]
public class Words : ScriptableObject
{
    public List<string> WordsList;
    public string ThemeName;
    public int WordsGuessedNum;
    public int StartWordSize;

    public bool CurrentlyPlaying;
    public bool ThemeDeactivated;

    public List<string> SavedWords;
    public List<int> RightLetters;
    public List<int> WrongLetters;

    private void OnEnable()
    {
        StartWordSize = WordsList.Count;
        ResetThemeWords();
    }
    public void ResetThemeWords()
    {
        if (SavedWords.Count < 0) 
        {
            ThemeDeactivated = false;
            return;
        }
        for (int i = 0; i < SavedWords.Count; i++)
        {
            WordsList.Add(SavedWords[0]);
            SavedWords.RemoveAt(0);
            RightLetters.Clear();
            WrongLetters.Clear();
        }
        
    }
    public void SaveWordInfo(string word, int rightLetters, int wrongLetters)
    {
       SavedWords.Add(word);
       RightLetters.Add(rightLetters);
       WrongLetters.Add(wrongLetters);
    }
    public List<string> ShuffleMyList()
    {
        WordsList.ListShuffle();
        return WordsList;
    }
}
