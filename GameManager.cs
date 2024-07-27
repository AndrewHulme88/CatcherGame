using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int remainingCount;
    public TMP_Text scoreText;
    public TMP_Text countDownText;
    public TMP_Text timerText;


    private LevelManager levelManager;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

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

    public void InitializeCountUI()
    {
        if (countDownText != null)
        {
            countDownText.text = remainingCount.ToString() + " Items Remaining";
        }
    }

    public void UpdateCountUI()
    {
        remainingCount--;
        if (countDownText != null)
        {
            countDownText.text = remainingCount.ToString() + " Items Remaining";
        }
    }

    public void UpdateTimerUI(float timeLeft)
    {
        float seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = string.Format("{00}", seconds);
    }
}
