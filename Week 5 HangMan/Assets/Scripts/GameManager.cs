using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private int stickManlives; //check
    [SerializeField] private int Score; //check
    
    [Header("Pop Up Panels")]
    [SerializeField] private GameObject mainMenuObj;
    [SerializeField] private GameObject chooseThemePanel;
    [SerializeField] private GameObject gameEnded;
    [SerializeField] private GameObject youLosePanel;
    [SerializeField] private GameObject chooseThemeWarning;
    [SerializeField] private GameObject menuObj;
    public GameObject playAgainField;

    [Header("GameObjects Needed")]
    [SerializeField] private GameObject questionField;
    [SerializeField] private GameObject keyboardParent;
    [SerializeField] private GameObject drawManObj;
    [SerializeField] private Animator warningPopAnim;

    [Header("Buttons")]
    [SerializeField] private GameObject anotherChanceButton;
    [SerializeField] private GameObject hintButtonObj;
    [SerializeField] private GameObject menuButtonObj;
    [SerializeField] private Button startButton; 

    [Header("Scripts")]
    [SerializeField] private WordManager wordManager;
    [SerializeField] private KeyBoard keyboard;
    [SerializeField] private DrawMan drawMan;

    public  bool gameStarted { get; private set; }

    private void Start()
    {
        playerInfo.ResetGame();
        mainMenuObj.SetActive(true);
        playerInfo.ThemeChosen = false;
        gameStarted = false;
    }

    public void StartGame()
    {
        if (!playerInfo.ThemeChosen)
        {
            StartCoroutine(ChooseThemeWarningCoroutine());
            return;
        }
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
    public void YouLost()
    {
        ToggleUI("LosePanel");
    }

    public void ToggleUI(string gameobjectName)
    {
        switch (gameobjectName)
        {
            case "LosePanel":
                if (youLosePanel.activeSelf)
                {
                    youLosePanel.SetActive(false);
                    anotherChanceButton.SetActive(false);
                }
                else if (!youLosePanel.activeSelf)
                {
                    youLosePanel.SetActive(true);
                    if (playerInfo.PlayerLives > 0)
                    {
                       anotherChanceButton.SetActive(true);
                    }
                }
                break;

            case "Menu":
                if (menuObj.activeSelf)
                {
                    menuObj.SetActive(false);
                    ActivateKeyboardButtons();
                }
                else
                {
                    DeactivateKeyboardButtons();
                    menuObj.SetActive(true);
                }
                break;

            case "ChooseTheme":
                chooseThemePanel.SetActive(true);
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
    // UI Button Scripts
    
    
    public void MainMenuButton()
    {
        menuObj.SetActive(false);
        DeactivateAllGamePanels();
        mainMenuObj.SetActive(true);
    }
    public void QuitGameButton()
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
    public void WrongLetterChacked() //not ui
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
        DeactivateKeyboardButtons();
    }

    public void ChooseAnotherTheme()
    {
        gameEnded.SetActive(false);
        chooseThemePanel.SetActive(true);
        DeactivateKeyboardButtons();
    }

    public void OpenThemePanel()
    {
        DeactivateKeyboardButtons();
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
            if (drawMan.gameObject.activeSelf)
            {
                drawMan.UpdateStickmanState();
            }
        }
        else if (!chooseThemePanel.GetComponent<ChooseTheme>().ThemeCurrentlyPlaying)
        {
            chooseThemePanel.SetActive(false);
            playerInfo.StickManLives = 0;
            if (drawMan.gameObject.activeSelf)
            {
               drawMan.UpdateStickmanState();
            }
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
        DeactivateKeyboardButtons();
    }
    public void SaveScores()
    {
        playerInfo.AddWordGuessedNum();
    }
    private void DeactivateKeyboardButtons()
    {
        for (int i = 0; i < keyboardParent.transform.childCount; i++)
        {
            keyboardParent.transform.GetChild(i).GetComponent<ButtonScrt>().DeactivateButton();
        }
    }
    private void ActivateKeyboardButtons()
    {
        for (int i = 0; i < keyboardParent.transform.childCount; i++)
        {
            keyboardParent.transform.GetChild(i).GetComponent<ButtonScrt>().ActivateButtons();
        }
    }
    private IEnumerator ChooseThemeWarningCoroutine()
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
