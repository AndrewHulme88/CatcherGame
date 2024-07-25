using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
