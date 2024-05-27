using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [Header("TMP Objects")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Text headers")]
    [SerializeField] private string scoreHeader = "Score: ";
    [SerializeField] private string highScoreHeader = "Highscore: ";

    public void UpdateHighScore(int highScore)
    {
        highScoreText.text = highScoreHeader + highScore.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = scoreHeader + score.ToString();
    }
}
