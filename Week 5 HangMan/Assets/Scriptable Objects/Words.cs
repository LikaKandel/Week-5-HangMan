
using UnityEngine;

[CreateAssetMenu(menuName = "Words Theme", fileName = "New Words Theme")]
public class Words : ScriptableObject
{
    public string[] words;

    public string[] ShuffleMyArray()
    {
        ShuffleArray.Shuffle(words);
        return words;
    }
}
