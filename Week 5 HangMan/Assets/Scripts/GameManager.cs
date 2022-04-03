using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.UI;

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
    [SerializeField] private GameObject heartPanel;
    [SerializeField] private GameObject gameObjectsPanel;
    [SerializeField] private GameObject quitGameWarning;
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
    [SerializeField] private Themes themes;
    private bool _hasReadInfo;

    public  bool gameStarted { get; private set; }
    private int _currentThemeNum;

    private void Start()
    {
        playerInfo.ResetGame();
    }
    public void StartPlaying()
    {
        if (!playerInfo.ThemeChosen)
        {
            StartCoroutine(ChooseThemeWarningCoroutine());
            return;
        }
        mainMenuObj.SetActive(false);
        gameObjectsPanel.SetActive(true);
        playerInfo.NoMistakesThisRound = true;
        gameStarted = true;
        _hasReadInfo = false;
        _currentThemeNum = playerInfo.ThemeNum;
        _wantsQuit = false;
        _wantsOtherTheme = false;

        if (gameStarted)
        {
            keyboard.SpawnNewBoard();
        }
    }

    public void StartMenu()
    {
        playerInfo.ResetGame();
        mainMenuObj.SetActive(true);
        heartPanel.SetActive(true);
        gameStarted = false;
    }
    public void YouLost()
    {
        keyboard.DeactivateAllButtons();
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
    }

    public void ToggleUI(string gameobjectName)
    {
        switch (gameobjectName)
        {
            case "Menu":
                if (menuObj.activeSelf)
                {
                    menuObj.SetActive(false);
                }
                else
                {
                    menuObj.SetActive(true);
                }
                break;

            case "ChooseTheme":
                chooseThemePanel.SetActive(true);
                break;

        }
    }
    public void ToggleObj(GameObject obj)
    {
        if (obj.activeSelf) obj.SetActive(false);
        else obj.SetActive(true);
    }
    public void OneMoreChance() // another chance button when "lost game"
    {
        keyboard.ActivateAllButtons();
        youLosePanel.SetActive(false);
        playerInfo.GiveOneMoreChance();
        drawMan.UpdateStickmanState();
        playerInfo.NoMistakesThisRound = false;
    }
    // UI Button Scripts
    
    
    public void MainMenuButton()
    {
        menuObj.SetActive(false);
        gameObjectsPanel.SetActive(false);
        mainMenuObj.SetActive(true);
    }

    public void ToggleMenu()
    {
        if (menuObj.activeSelf == true)
        {
            menuObj.SetActive(false);
            //keyboard.ActivateAllButtons();
        }
        else
        {
            menuObj.SetActive(true);
            keyboard.DeactivateAllButtons();
        }
    }
    public void NextWord()
    {
        playAgainField.SetActive(false);
        if (playerInfo.StickManLives < 0) return;
        drawMan.UpdateStickmanState();
    }
    public void WrongLetterChecked() 
    {
        drawMan.UpdateStickmanState();
        if (playerInfo.StickManLives >= stickManlives) YouLost();
    }

    public void PlayAgainPanel()
    {
        playAgainField.SetActive(true);
        keyboard.DeactivateAllButtons();
    }
    public void ChooseAnotherTheme()
    {
        gameEnded.SetActive(false);
        chooseThemePanel.SetActive(true);
        menuObj.SetActive(false);
    }
    private bool _wantsQuit;
    public void QuitGameButton()
    {
        _wantsQuit = true;
        if (playerInfo.HasStartedGuessing && !_hasReadInfo)
        {
            quitGameWarning.SetActive(true);
            _hasReadInfo = true;
            return;
        }
        gameObjectsPanel.SetActive(false);
        gameStarted = false;
        playerInfo.ResetGame();
        menuObj.SetActive(false);
        mainMenuObj.SetActive(true);
        _hasReadInfo = false;

    }
    
    private bool _wantsOtherTheme;
    public void OpenThemePanel()
    {
        _wantsOtherTheme = true;
        gameObjectsPanel.SetActive(false);
        menuObj.SetActive(false);
        mainMenuObj.SetActive(false);
        chooseThemePanel.SetActive(true);
        _hasReadInfo = false;
    }
    public void QuitCurGame()
    {
        if (_wantsOtherTheme)
        {
            playerInfo.StickManLives = 0;
            drawMan.UpdateStickmanState();
            chooseThemePanel.SetActive(false);
            gameObjectsPanel.SetActive(true);
            quitGameWarning.SetActive(false);
            _wantsOtherTheme = false;
            keyboard.SpawnNewBoard();
        }
        else if (_wantsQuit)
        {
            playerInfo.StickManLives = 0;
            drawMan.UpdateStickmanState();
            gameObjectsPanel.SetActive(false);
            quitGameWarning.SetActive(false);
            gameStarted = false;
            playerInfo.ResetGame();
            menuObj.SetActive(false);
            mainMenuObj.SetActive(true);
            _wantsQuit = false;
            playerInfo.ResetGame();

        }
    }
    public void ContinueGame()
    {
        _hasReadInfo = false;
        menuObj.SetActive(false);

    }
    public void ChooseThemeButt()
    {
        if (!gameStarted)
        {
            mainMenuObj.SetActive(true);
            chooseThemePanel.SetActive(false);
            playerInfo.StickManLives = 0;
        }
        else if (gameStarted && playerInfo.ThemeNum != _currentThemeNum)
        {
            if (playerInfo.HasStartedGuessing && !_hasReadInfo)
            {
                quitGameWarning.SetActive(true);
                _hasReadInfo = true;
                return;
            }
            chooseThemePanel.SetActive(false);
            StartPlaying();
            wordManager.StartGame();
            keyboard.SpawnNewBoard();

        }
        else if (playerInfo.ThemeNum == _currentThemeNum)
        {
            gameObjectsPanel.SetActive(true);
            chooseThemePanel.SetActive(false);
            menuObj.SetActive(false);
            mainMenuObj.SetActive(false);//have to check
        }
    }
    public void ThemeFinished()
    {
        if (playerInfo.HasFinishedCurrentTheme)
        {
            //add go to menu button
        }
        playAgainField.SetActive(false);
        gameEnded.SetActive(true);
        hintButtonObj.SetActive(false);
        menuButtonObj.SetActive(false);
        keyboard.DeactivateAllButtons();
    }
    public void SaveScores()
    {
        //playerInfo.AddWordGuessedNum();
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
    
}
