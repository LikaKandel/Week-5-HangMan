using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentTheme : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    [SerializeField] private PlayerInfo playerInfo;

    private void Start()
    {
        buttonText.text = "THEME: SELECT";
    }
    public void DisplayChosenTheme()
    {
        buttonText.text = "THEME: " + playerInfo.ThemeName;
    }
}
