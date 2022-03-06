using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject gameobjectWithScripts;
    [SerializeField] private WordManager wordManager;
    [SerializeField] private KeyBoard keyboard;
    [SerializeField] private HintFunction hintScript;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = gameobjectWithScripts.GetComponent<GameManager>();
    }

    public void Retry()
    {
        wordManager.WordCount++;
        gameManager.OneMoreChance();
        wordManager.RetryWhenLost();
    }

    public void Menu()
    {
        wordManager.ClearChildren();
        wordManager.ExitingCurrentGame();
        keyboard.ClearAll();
        gameManager.ChooseAnotherTheme();
        gameManager.ToggleUI("Menu");
        hintScript.ResetHints();
        
    }

    public void ChooseTheme()
    {
        wordManager.ClearChildren();
        keyboard.ClearAll();
        wordManager.ExitingCurrentGame();
        gameManager.ChooseAnotherTheme();
        hintScript.ResetHints();
    }
}
