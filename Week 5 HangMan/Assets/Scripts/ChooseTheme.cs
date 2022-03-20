using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseTheme : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Themes themes;
    public int themeNum;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject horizontalPanelPrefab;
    [SerializeField] private Transform buttonParent;

    
    [SerializeField] private Words[] themeArray;
    
    public void InstantiateHorizontalPanels()
    {
        int themeNum = themes.WordThemes.Length;
        int buttonsInPanel = 0;
        GameObject panel;
        panel = (GameObject)Instantiate(horizontalPanelPrefab, buttonParent);
        for (int i = 0; i < themeNum; i++)
        {
            if (buttonsInPanel == 4)
            {
               panel = (GameObject)Instantiate(horizontalPanelPrefab, buttonParent);
                buttonsInPanel = 0;
            }
            SpawnThemeButtons(panel.transform, i);
            buttonsInPanel++;
        }
    }
    private void SpawnThemeButtons(Transform parent, int themeNum)
    {
        var curButton = (GameObject)Instantiate(buttonPrefab, parent);
        curButton.GetComponent<ThemeButton>().AddButtonInfo(themeNum);

    }


}
