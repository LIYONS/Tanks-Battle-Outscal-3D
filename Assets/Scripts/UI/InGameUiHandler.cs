using UnityEngine;
using TMPro;
using TankGame.GameManagers;
using TankGame.Achievements;

namespace TankGame.UI
{
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
        private SceneManager gameManager;
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
            EventManager.Instance.OnEnemyDeath += UpdateScore;
            EventManager.Instance.OnGameOver += OnGameOver;
        }
        private void Start()
        {
            achievementPanel.SetActive(false);
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            InitializeScores();
            gameManager = SceneManager.Instance;
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
            if (currentScore > currentHighScore)
            {
                UpdateHighScore();
            }
        }
        private void PlayAchievementSound()
        {
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.Achievement);
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
            EventManager.Instance.OnEnemyDeath -= UpdateScore;
            EventManager.Instance.OnGameOver -= OnGameOver;
        }

        public void OnButtonClick()
        {
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.ButtonClick);
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
                audioManager.StopAudio(AudioSourceType.Music);
                audioManager.StopAudio(AudioSourceType.Game);
            }
        }
        public void OnPauseButtonPress()
        {
            if (pausePanel.activeInHierarchy)
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
            if (audioManager)
            {
                audioManager.ResetSounds();
            }
            Time.timeScale = 1f;
        }

        public void LoadMainMenu()
        {
            if (gameManager)
            {
                gameManager.LoadMainMenu();
                if (audioManager)
                {
                    audioManager.StopAudio(AudioSourceType.Game);
                }
            }
        }
        public void ReStart()
        {
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.BackGroundMusic);
                audioManager.PlaySound(SoundType.TankIdle);
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
}
