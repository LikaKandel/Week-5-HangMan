using UnityEngine;
using UnityEngine.UI;

public class DisplayWordInfo : MonoBehaviour
{
    [SerializeField] private Text word;
    [SerializeField] private Text rightAnswerAmount;
    [SerializeField] private Text wrongAnswerAmount;


    public void DisplayInfo(string _word, int rightAnswers, int wrongAnswers)
    {
        print("displayInfo");
        word.text = $"{_word}";
        rightAnswerAmount.text = $"{rightAnswers}";
        wrongAnswerAmount.text = $"{wrongAnswers}";
    }
}
