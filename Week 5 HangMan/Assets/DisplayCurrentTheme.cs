using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentTheme : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    [SerializeField] private PlayerInfo playerInfo;

    private void OnEnable()
    {
        if (!playerInfo.ThemeChosen)
        {
            buttonText.text = "THEME: SELECT";
        }
        else DisplayChosenTheme();
    }
    public void DisplayChosenTheme()
    {
        buttonText.text = "THEME: " + playerInfo.ThemeName;
    }
}
