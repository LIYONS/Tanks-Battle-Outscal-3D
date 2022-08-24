using UnityEngine;
using TMPro;

public class InGameUiHandler : MonoBehaviour
{
    [SerializeField] private int scoreForKill;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private TextMeshProUGUI achievementDescription;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private float achievemntShowTimer;

    //Pause
    [SerializeField] private GameObject pausePanel;
    private GameManager gameManager;
    private AudioManager audioManager;

    //GameOver
    [SerializeField] private GameObject gameOverPanel;

    private int currentScore;
    private int currentHighScore;
    private const string defaultText = "SCORE - ";
    private const string highScore = "highScore";
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
        InitializeScores();
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        Time.timeScale = 1f;
    }
    private void InitializeScores()
    {
        currentScore = 0;
        scoreText.text = defaultText + currentScore;
        if (PlayerPrefs.HasKey(highScore))
        {
            currentHighScore = PlayerPrefs.GetInt(highScore);
        }
        else
        {
            currentHighScore = 0;
            PlayerPrefs.SetInt(highScore, currentHighScore);
            PlayerPrefs.Save();
        }
        highScoreText.text = "HIGH SCORE  : " + currentHighScore;
    }

    private void UpdateHighScore()
    {
        currentHighScore = currentScore;
        highScoreText.text = "HIGH SCORE  : " + currentHighScore;
    }
    private void UpdateScore()
    {
        currentScore += scoreForKill;
        scoreText.text = defaultText + currentScore;
        if(currentScore>currentHighScore)
        {
            UpdateHighScore();
        }
    }
    private void PlayAchievementSound()
    {
        if (audioManager)
        {
            audioManager.PlaySfx(SoundType.Achievement);
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
        if (audioManager)
        {
            audioManager.PlaySfx(SoundType.ButtonClick);
        }
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
        StopSounds();
        PlayerPrefs.SetInt(highScore, currentHighScore);
    }

    public void StopSounds()
    {
        if (audioManager)
        {
            audioManager.StopMusic();
            audioManager.StopGameMusic();
        }
    }
    public void OnPauseButtonPress()
    {
        if(pausePanel.activeInHierarchy)
        {
            Resume();
            return;
        }
        pausePanel.SetActive(true);
        if (audioManager)
        {
            audioManager.Mute();
        }
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        if(audioManager)
        {
            audioManager.ResetSounds();
        }
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        if(gameManager)
        {
            gameManager.LoadMainMenu();
            if(audioManager)
            {
                audioManager.StopGameMusic();
            }
        }
    }
    public void ReStart()
    {
        if (audioManager)
        {
            audioManager.PlayMusic(SoundType.BackGroundMusic);
            audioManager.PlayGameSound(SoundType.TankIdle);
            audioManager.ResetSounds();
        }
        if (gameManager)
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
