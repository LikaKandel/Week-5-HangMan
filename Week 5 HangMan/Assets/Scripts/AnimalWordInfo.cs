using UnityEngine;
using UnityEngine.UI;

public class AnimalWordInfo : MonoBehaviour
{
    [SerializeField] private Text word;
    [SerializeField] private Text rightAnswerAmount;
    [SerializeField] private Text wrongAnswerAmount;


    public void DisplayInfo(string _word, int _rightAnswers, int _wrongAnswers)
    {
        word.text = _word;
        rightAnswerAmount.text = _rightAnswers.ToString();
        wrongAnswerAmount.text = _wrongAnswers.ToString();
    }
}
