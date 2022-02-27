using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject questionField;
    
    [SerializeField] private GameObject chooseThemePanel;
    [SerializeField] private GameObject youLosePanel;
    [SerializeField] private GameObject keyboardParent;
    [SerializeField] private GameObject anotherChanceButton;
    [SerializeField] private GameObject gameEnded;
    [SerializeField] private GameObject drawManObj;
    [SerializeField] private GameObject hintButtonObj;
    [SerializeField] private GameObject menuButtonObj;
    [SerializeField] private GameObject chooseThemeWarning;
    [SerializeField] private GameObject mainMenuObj;
    [SerializeField] private GameObject menuObj;

    [SerializeField] private Button startButton; 
     private Animator warningPopAnim;

    [SerializeField] private int stickManlives;
    [SerializeField] private int Score;


    [SerializeField] private KeyBoard keyboard;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private WordManager wordManager;
    public GameObject playAgainField;

    private DrawMan drawMan;
    public  bool gameStarted { get; private set; }
    private void Start()
    {
        playerInfo.ResetGame();
        drawMan = drawManObj.GetComponent<DrawMan>();
        playerInfo.ThemeChosen = false;
        gameStarted = false;
        mainMenuObj.SetActive(true);

        warningPopAnim = chooseThemeWarning.GetComponent<Animator>();
    }

    public void StartGame()
    {
        if (playerInfo.ThemeChosen)
        {
            drawManObj.SetActive(true);
            chooseThemePanel.SetActive(false);
            questionField.SetActive(true);
            keyboardParent.SetActive(true);
            hintButtonObj.SetActive(true);
            menuButtonObj.SetActive(true);
            playerInfo.NoMistakesThisRound = true;
            mainMenuObj.SetActive(false);
            gameStarted = true;

            wordManager.SelectNewWord();
            keyboard.ChooseLettersNeeded(playerInfo.GetCurrentWord());
        }
        else StartCoroutine(ChooseThemeWarning());
        
    }
    public void YouLost()
    {
        OpenPanel("LosePanel");
    }

    private void OpenPanel(string popName)
    {
        switch (popName)
        {
            case "LosePanel":
                youLosePanel.SetActive(true);
                if (playerInfo.PlayerLives > 0) anotherChanceButton.SetActive(true);
                break;
        }
    }
    public void OneMoreChance() // another chance button when "lost game"
    {
        youLosePanel.SetActive(false);
        playerInfo.GiveOneMoreChance();
        drawMan.UpdateStickmanState();
        playerInfo.NoMistakesThisRound = false;
    }
    public void Menu()
    {
        DeactivateButtons();
        menuObj.SetActive(true);
    }
    public void ContinueGame()
    {
        ActivateButtons();
        menuObj.SetActive(false);
    }

    

    public void MainMenu()
    {
        menuObj.SetActive(false);
        DeactivateAllGamePanels();
        mainMenuObj.SetActive(true);
    }
    public void QuitGame()
    {
        DeactivateAllGamePanels();
        gameStarted = false;
        playerInfo.ResetGame();
        menuObj.SetActive(false);
        mainMenuObj.SetActive(true);
        playerInfo.ResetGame();

    }
    public void CheckWrongValue()
    {
        if (playerInfo.NoMistakesThisRound && playerInfo.StickManLives > 0)
        {
            playerInfo.RemoveManPart();
            drawMan.UpdateStickmanState();
        }
    }
    public void WrongLetterChacked()
    {
        playerInfo.WrongValue++;
        playerInfo.AddBodyPart();
        playerInfo.NoMistakesThisRound = false;
        drawMan.UpdateStickmanState();
        if (playerInfo.WrongValue >= stickManlives) YouLost();
    }

    public void PlayAgainPanel()
    {
        playAgainField.SetActive(true);
        DeactivateButtons();
    }

    public void ChooseAnotherTheme()
    {
        gameEnded.SetActive(false);
        chooseThemePanel.SetActive(true);
        DeactivateButtons();
    }

    public void OpenThemePanel()
    {
        DeactivateButtons();
        menuObj.SetActive(false);
        DeactivateAllGamePanels();
        mainMenuObj.SetActive(false);
        chooseThemePanel.SetActive(true);
    }
    public void CloseThemePanel()
    {
        if (!gameStarted)
        {
            mainMenuObj.SetActive(true);
            chooseThemePanel.SetActive(false);
            playerInfo.StickManLives = 0;
            drawMan.UpdateStickmanState();
        }
        else if (!chooseThemePanel.GetComponent<ChooseTheme>().ThemeCurrentlyPlaying)
        {
            chooseThemePanel.SetActive(false);
            playerInfo.StickManLives = 0;
            drawMan.UpdateStickmanState();
        }
        else
        {
            chooseThemePanel.SetActive(false);
            menuButtonObj.SetActive(true);
        }

    }
    public void ThemeFinished()
    {
        playAgainField.SetActive(false);
        gameEnded.SetActive(true);
        hintButtonObj.SetActive(false);
        menuButtonObj.SetActive(false);
        DeactivateButtons();
    }
    public void SaveScores()
    {
        playerInfo.AddWordGuessedNum();
    }
    private void DeactivateButtons()
    {
        for (int i = 0; i < keyboardParent.transform.childCount; i++)
        {
            keyboardParent.transform.GetChild(i).GetComponent<ButtonScrt>().DeactivateButton();
        }
    }
    private void ActivateButtons()
    {
        for (int i = 0; i < keyboardParent.transform.childCount; i++)
        {
            keyboardParent.transform.GetChild(i).GetComponent<ButtonScrt>().ActivateButtons();
        }
    }
    private IEnumerator ChooseThemeWarning()
    {
        startButton.interactable = false;
        chooseThemeWarning.SetActive(true);
        warningPopAnim.enabled = true;
        yield return new WaitForSeconds(3);
        chooseThemeWarning.SetActive(false);
        startButton.interactable = true;
        warningPopAnim.enabled = false ;
    }
    private void DeactivateAllGamePanels()
    {
        chooseThemePanel.SetActive(false);
        youLosePanel.SetActive(false);
        keyboardParent.SetActive(false);
        anotherChanceButton.SetActive(false);
        gameEnded.SetActive(false);
        drawManObj.SetActive(false);
        hintButtonObj.SetActive(false);
        menuButtonObj.SetActive(false);
    }
}
