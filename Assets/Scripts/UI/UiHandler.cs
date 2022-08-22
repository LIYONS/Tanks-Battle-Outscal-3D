using UnityEngine;
using TMPro;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private int scoreForKill;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private TextMeshProUGUI achievementDescription;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private float achievemntShowTimer;

    private int totalScore;
    private const string defaultText = "Score - ";
    private const string defaultAchievementText = "Achievement Unlocked : ";

    private void OnEnable()
    {
        EventHandler.Instance.OnEnemyDeath += UpdateScore;
    }
    private void Start()
    {
        achievementPanel.SetActive(false);
        totalScore = 0;
        scoreText.text = defaultText + totalScore;
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
    }
}
