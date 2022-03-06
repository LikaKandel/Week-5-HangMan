using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Words Theme", fileName = "New Words Theme")]
public class Words : ScriptableObject
{
    public List<string> words;

    public List<string> ShuffleMyList()
    {
        words.ListShuffle();
        return words;
    }
}
