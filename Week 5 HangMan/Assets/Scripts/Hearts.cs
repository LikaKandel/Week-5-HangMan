using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hearts : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite[] heartImages;
    [SerializeField] private GameObject[] heartObjs;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private float startReganarationTimeMinutes;
    [SerializeField] private Text countdownText;

    [SerializeField] private Transform boxTransform;
    [SerializeField] private Text textTransform;

    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private Animator animator;
    private float timeReganarationNum;
    private bool mouse_over;
    private bool doAnimate;

    private bool _countDown;

    private const int _playerStartLives = 3;

    private void Start()
    {
        timeReganarationNum = startReganarationTimeMinutes;
        //float minutesToSeconds = startReganarationTimeMinutes *= 60;
        //timeReganarationNum = minutesToSeconds;
        doAnimate = false;
    }
    private void Update()
    {
        if (_countDown)
        {
            timeReganarationNum -= Time.deltaTime;

            if (timeReganarationNum <= 0)
            {
                timeReganarationNum = startReganarationTimeMinutes;
                IncreaseLives();
            }
        }
        if (mouse_over)
        {
            doAnimate = true;
            if (playerInfo.PlayerLives < _playerStartLives) DisplayTimeLeft(timeReganarationNum);
            else if (playerInfo.PlayerLives == _playerStartLives)
            {
                countdownPanel.SetActive(true);
                countdownText.text = "FULL";
            }
        }
        else if (!mouse_over && doAnimate)StartCoroutine(FadeOut());
    }
    private void IncreaseLives()
    {
        heartObjs[playerInfo.PlayerLives].SetActive(true);
        playerInfo.PlayerLives++;
        if (playerInfo.PlayerLives == _playerStartLives)
        {
            _countDown = false;
        }
    }
    public void DecreaseLives()
    {
        playerInfo.PlayerLives--;
        heartObjs[playerInfo.PlayerLives].SetActive(false);
        print(heartObjs[playerInfo.PlayerLives].name);
        _countDown = true;
        if (_countDown)
        {
            print("countdown");
        }
    }

    private void DisplayTimeLeft(float curTime)
    {
        countdownPanel.SetActive(true);
        float minutes = (int)(curTime / 60);
        float seconds = (int)(curTime % 60);

        countdownText.text =  minutes.ToString("00") + " : " + seconds.ToString("00");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
    private IEnumerator FadeOut()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(0.4f);
        doAnimate = false;
        animator.enabled = false;
        countdownPanel.SetActive(false);
        RestoreUITrancparancy();
    }

    private void RestoreUITrancparancy()
    {
        Color color = new Color(1, 1, 1, 1);
        boxTransform.GetComponent<Image>().color = color;
        textTransform.color = color;
    }

}
