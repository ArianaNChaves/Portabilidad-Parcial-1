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

    [Header("End-Game Panels")]
    public GameObject   winPanel;
    public GameObject   losePanel;

    [Header("Close Win Panel")]
    // Drag in the RectTransform of your Win Panel here
    public RectTransform winPanelRect;

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

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        clickButton.interactable = true;

        clickButton.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        // 1) If Win Panel is up, catch clicks outside of its box
        if (winPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            Vector2 msPos = Input.mousePosition;
            // null camera works for Screen Space – Overlay canvases
            if (!RectTransformUtility.RectangleContainsScreenPoint(winPanelRect, msPos, null))
            {
                winPanel.SetActive(false);
            }
        }

        // 2) Normal timer logic
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

    void OnButtonClick()
    {
        if (!timerRunning && Mathf.Approximately(timeRemaining, 10f))
            timerRunning = true;

        if (timerRunning)
        {
            clickCount++;
            countText.text = clickCount.ToString();
        }
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
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    string FormatTime(float t)
    {
        int m = Mathf.FloorToInt(t / 60f);
        int s = Mathf.FloorToInt(t % 60f);
        return $"{m:00}:{s:00}";
    }
}
