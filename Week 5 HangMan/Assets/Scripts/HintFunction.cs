using UnityEngine;
using UnityEngine.UI;

public class HintFunction : MonoBehaviour
{
    [SerializeField]private WordManager wordManager;
    [SerializeField]private KeyBoard keyBoard;
    [SerializeField] private Transform keyboardParnt;
    [SerializeField] private int startHintAmount;
    [SerializeField] private Text hintNumText;

    private string _word;
    private int hintAmount;
    private int lastLoopNum = 0;

    private bool canGiveHint;
    private void Start()
    {
        hintAmount = startHintAmount;
        DisplayHintsLeft();
    }

    public void GetHint()
    {
        TakeCurrentWord();
        CheckCorrectButton();
        DisplayHintsLeft();
    }
    private void TakeCurrentWord()
    {
        _word = WordManager.currentWord; // its get; private set;
    }
    
    private void CheckCorrectButton()
    {
        for (int i = lastLoopNum; i < keyboardParnt.childCount; i++)
        {
            lastLoopNum++;
            ButtonScrt key = keyboardParnt.GetChild(i).gameObject.GetComponent<ButtonScrt>();
            if (_word.Contains(key.thisLetter) && wordManager.hintsThisRound < 1)
            {
                key.DrawYellowLine();
                wordManager.GetHintLetter(key.thisLetter, hintAmount);
                hintAmount--;
                break;
            }
        }
    }
    public void ResetHints()
    {
        hintAmount = startHintAmount;
        DisplayHintsLeft();
    }

    private void DisplayHintsLeft()
    {
        hintNumText.text = "HINT : " + hintAmount.ToString();
    }
}
