using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseTheme : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Themes themes;
    public int ThemeNum{ get; private set; }

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject horizontalPanelPrefab;
    [SerializeField] private Transform buttonParent;

    
    [SerializeField] private Words[] themeArray;
    [SerializeField]private List<ThemeButton> _themeButtons;

    

    private void Awake()
    {
        InstantiateHorizontalPanels();
    }
    private void InstantiateHorizontalPanels()
    {
        int themeNum = themes.WordThemes.Length;
        int buttonsInPanel = 0;
        var panel = Instantiate(horizontalPanelPrefab, buttonParent);
        for (int i = 0; i < themeNum; i++)
        {
            if (buttonsInPanel == 4)
            {
               panel = Instantiate(horizontalPanelPrefab, buttonParent);
                buttonsInPanel = 0;
            }
            SpawnThemeButtons(panel.transform, i);
            buttonsInPanel++;
        }
    }
    private void SpawnThemeButtons(Transform parent, int themeNum)
    {
        var curButton = Instantiate(buttonPrefab, parent);
        _themeButtons.Add(curButton.GetComponent<ThemeButton>());
        _themeButtons[themeNum].AddButtonInfo(themeNum);

        if (themes.WordThemes[themeNum].WordsList.Count > 0) _themeButtons[themeNum].CheckActivity(true);
        else _themeButtons[themeNum].CheckActivity(false);
    }
    private void UpdateButtonInfos()
    {
        for (int i = 0; i < _themeButtons.Count; i++)
        {
            print("updating info");
            _themeButtons[i].UpdateButtonInfo(i);
        }
    }
    public void RemoveBoxSprites()
    {
        for (int i = 0; i < _themeButtons.Count; i++)
        {
            _themeButtons[i].RemoveBoxSprite();
        }
    }

}
