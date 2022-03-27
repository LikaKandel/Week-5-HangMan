using UnityEngine;
using UnityEngine.UI;

public class ButtonScrt : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject redLineGameobject;
    [SerializeField] private GameObject greenLineGameobject;
    [SerializeField] private GameObject yellowLineGameobject;

    private Button thisButton;

    private WordManager wordManager;
    public string thisLetter { get; private set; }
    public bool isUsed { get; private set; }

    public int num { get; set; }

    private void Awake()
    {
        wordManager = FindObjectOfType<WordManager>();
    }
    private void OnEnable()
    {
        isUsed = false;
        thisButton = GetComponent<Button>();
    }
    public string InsertLetterInKey(string letter)
    {
        thisLetter = letter;
        _text.text = letter;
        return letter;
    }

    public void GetLetter()
    {
        wordManager.GetButtonInfo(thisButton);
    }

    public Button ReturnButton()
    {
        return thisButton;
    }

    public void DrawRedLine()
    {
        redLineGameobject.SetActive(true);
        thisButton.interactable = false;
    }

    public void DrawGreenLine() 
    {
        greenLineGameobject.SetActive(true);
        thisButton.interactable = false;
        isUsed = true;
    }
    public void DrawYellowLine()
    {
        yellowLineGameobject.SetActive(true);
        thisButton.interactable = false;
        isUsed = true;
    }
    public void DeactivateButton()
    {
        thisButton.interactable = false;
    }
    public void ActivateButton()
    {
        thisButton.interactable = false;
    }

}
