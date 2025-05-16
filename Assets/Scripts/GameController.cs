// Assets/Scripts/GameController.cs
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI References (TMP)")]
    public TMP_Text     timerText;
    public TMP_Text     countText;
    public TMP_Text     highscoreText;
    public Button       clickButton;
    public GameObject   creditsPanel;

    [Header("Managers")]
    public InterstitialManager interstitial;

    private float timeRemaining = 10f;
    private bool  timerRunning  = false;
    private int   clickCount    = 0;
    private int   highscore     = 0;
    private int   initialHigh;       

    void Start()
    {
        initialHigh            = PlayerPrefs.GetInt("Highscore", 0);
        highscore              = initialHigh;
        timeRemaining          = 10f;
        clickCount             = 0;

        timerText.text         = FormatTime(timeRemaining);
        countText.text         = clickCount.ToString();
        highscoreText.text     = highscore.ToString();
        
        clickButton.interactable = true;

        clickButton.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            EndGame();
        }

        timerText.text = FormatTime(timeRemaining);
    }

    public void OnButtonClick()
    {
        if (!timerRunning)
        {
            timerRunning = true;
        }

        clickCount++;
        countText.text = clickCount.ToString();
    }


    void EndGame()
    {
        clickButton.interactable = false;
        bool isNewHigh = clickCount > initialHigh;

        if (isNewHigh)
        {
            highscore = clickCount;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
            highscoreText.text = highscore.ToString();
        }
        else
        {
            interstitial.ShowInterstitial();
        }
        ResetGame();
    }

    string FormatTime(float t)
    {
        int m = Mathf.FloorToInt(t / 60f);
        int s = Mathf.FloorToInt(t % 60f);
        return $"{m:00}:{s:00}";
    }
    
    public void AddTime(float seconds)
    {
        timeRemaining += seconds;
        timerText.text = FormatTime(timeRemaining);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }
    
    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
    
    public void ResetGame()
    {
        initialHigh = PlayerPrefs.GetInt("Highscore", 0);
        highscore   = initialHigh;
    
        timeRemaining = 10f;
        timerRunning  = false;
        clickCount    = 0;
    
        timerText.text     = FormatTime(timeRemaining);
        countText.text     = clickCount.ToString();
        highscoreText.text = highscore.ToString();
    
        clickButton.interactable = true;
    }
}
