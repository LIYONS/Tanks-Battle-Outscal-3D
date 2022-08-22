using UnityEngine;
using TMPro;

public class InGameUiHandler : MonoBehaviour
{
    [SerializeField] private int scoreForKill;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private TextMeshProUGUI achievementDescription;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private float achievemntShowTimer;

    //Pause
    [SerializeField] private GameObject pausePanel;
    private GameManager gameManager;

    //GameOver
    [SerializeField] private GameObject gameOverPanel;

    private int totalScore;
    private const string defaultText = "Score - ";
    private const string defaultAchievementText = "Achievement Unlocked : ";

    private void OnEnable()
    {
        EventHandler.Instance.OnEnemyDeath += UpdateScore;
        EventHandler.Instance.OnGameOver += OnGameOver;
    }
    private void Start()
    {
        achievementPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        totalScore = 0;
        scoreText.text = defaultText + totalScore;
        gameManager = GameManager.Instance;
        Time.timeScale = 1f;
    }

    private void UpdateScore()
    {
        totalScore += scoreForKill;
        scoreText.text = defaultText + totalScore;
    }
    private void PlayAchievementSound()
    {
        var instance = AudioManager.Instance;
        if (instance)
        {
            instance.PlaySfx(SoundType.Achievement);
        }
    }
    public void OnAchievementUnlocked(AchievementScriptableObject achievement)
    {
        achievementText.text = defaultAchievementText + achievement.name;
        achievementDescription.text = achievement.achievementDescription;
        achievementPanel.SetActive(true);
        PlayAchievementSound();
        Invoke(nameof(DeactivateUi), achievemntShowTimer);
    }
    private void DeactivateUi()
    {
        achievementPanel.SetActive(false);
    }
    private void OnDisable()
    {
        EventHandler.Instance.OnEnemyDeath -= UpdateScore;
        EventHandler.Instance.OnGameOver -= OnGameOver;
    }

    public void OnButtonClick()
    {
        var instance = AudioManager.Instance;
        if (instance)
        {
            instance.PlaySfx(SoundType.ButtonClick);
        }
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void OnPauseButtonPress()
    {
        if(pausePanel.activeInHierarchy)
        {
            Resume();
            return;
        }
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        if(gameManager)
        {
            gameManager.LoadMainMenu();
        }
    }
    public void ReStart()
    {
        if(gameManager)
        {
            gameManager.ReStart();
        }
    }

    public void Quit()
    {
        if (gameManager)
        {
            gameManager.Quit();
        }
    }
}
