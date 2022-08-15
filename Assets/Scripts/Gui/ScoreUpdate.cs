using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] private int scoreForKill;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int totalScore;
    private const string defaultText = "Score - ";

    private void OnEnable()
    {
        EnemyTankView.OnEnemyDeath += UpdateScore;
    }
    private void Start()
    {
        totalScore = 0;
        scoreText.text = defaultText + totalScore;
    }

    private void UpdateScore()
    {
        totalScore += scoreForKill;
        scoreText.text = defaultText + totalScore;
    }

    private void OnDisable()
    {
        EnemyTankView.OnEnemyDeath -= UpdateScore;
    }
}
