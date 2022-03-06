using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject[] tutorialObjs;
    [SerializeField] private Button nextButton;
    [SerializeField] private Text buttonText;

    [SerializeField] private GameManager gameManager;

    private bool tutorialEnded;
    private int panelNum;
    private void Start()
    {
        panelNum = 0;
        tutorialEnded = false;
        GoToNextPage();
    }
    public void GoToNextPage()
    {
        if (tutorialEnded)
        {
            StartGame();
            return;
        }
        int lastPanelNum = tutorialObjs.Length; // its 3
        lastPanelNum -= 1;
        if (panelNum < lastPanelNum) // if panel num is less than 2
        {
            buttonText.text = "NEXT";
            tutorialObjs[panelNum].SetActive(true); //starts with 0
        }else if(panelNum == lastPanelNum)
        {
            tutorialObjs[panelNum].SetActive(true); //starts with 0
            buttonText.text = "START GAME";
            tutorialEnded = true;
        }
        panelNum++;
    }
    public void StartGame()
    {
        tutorialPanel.SetActive(false);
        gameManager.StartMenu();
    }
}
