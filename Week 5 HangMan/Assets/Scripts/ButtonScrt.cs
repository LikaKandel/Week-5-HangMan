using UnityEngine;
using UnityEngine.UI;

public class ButtonScrt : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject redLineGameobject;
    [SerializeField] private GameObject greenLineGameobject;
    [SerializeField] private GameObject yellowLineGameobject;
    private WordManager wordManager;
    public string thisLetter { get; private set; }
    public bool isUsed { get; private set; }
    public int num;

    private void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
    }
    private void OnEnable()
    {
        isUsed = false;
    }
    public string InsertLetterInKey(string letter)
    {
        thisLetter = letter;
        _text.text = letter;
        return letter;
    }

    public void GetLetter()
    {
        Button button = GetComponent<Button>();
        wordManager.GetButtonLetter(button);
    }

    public Button ReturnButton()
    {
        Button button = GetComponent<Button>();
        return button;
    }

    public void DrawRedLine()
    {
        redLineGameobject.SetActive(true);
        GetComponent<Button>().interactable = false;
    }

    public void DrawGreenLine() 
    {
        greenLineGameobject.SetActive(true);
        GetComponent<Button>().interactable = false;
        isUsed = true;
    }
    public void DrawYellowLine()
    {
        yellowLineGameobject.SetActive(true);
        GetComponent<Button>().interactable = false;
        isUsed = true;
    }
    public void DeactivateButton()
    {
        GetComponent<Button>().interactable = false;
    }
    public void ActivateButtons()
    {
        GetComponent<Button>().interactable = true;
    }

}
