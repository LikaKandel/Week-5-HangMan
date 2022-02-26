using UnityEngine;
using UnityEngine.UI;

public class DrawMan : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Sprite[] sprites;
    private Image img;
    
    private void Start()
    {
        img = GetComponent<Image>();
        img.sprite = sprites[playerInfo.StickManLives];
    }
    public void UpdateStickmanState()
    {
        img.sprite = sprites[playerInfo.StickManLives];
    }
}
