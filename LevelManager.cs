using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isBonusLevel = false;
    [SerializeField] private float initialTimeLeft = 20f;

    public int currentCount = 20;
    public string nextSceneName;

    private GameManager gameManager;
    private float currentTimeLeft;
    private bool isTimerRunning = false;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        if(!isBonusLevel)
        {
            gameManager.timerText.gameObject.SetActive(false);
            gameManager.remainingCount = currentCount;
            gameManager.InitializeCountUI();
        }
        else
        {
            gameManager.timerText.gameObject.SetActive(true);
            currentTimeLeft = initialTimeLeft;
            isTimerRunning = true;
            gameManager.UpdateTimerUI(currentTimeLeft);
            gameManager.remainingCount = currentCount;
            gameManager.InitializeCountUI();
        }
    }

    private void Update()
    {
        if(!isBonusLevel) 
        {
            if (currentCount <= 0)
            {
                NextLevel();
            }            
        }
        else
        {
            if (currentCount <= 0)
            {
                NextLevel();
            }

            if (isTimerRunning)
            {
                if(currentTimeLeft >0)
                {
                    currentTimeLeft -= Time.deltaTime;
                    gameManager.UpdateTimerUI(currentTimeLeft);
                }
                else
                {
                    currentTimeLeft = 0;
                    isTimerRunning = false;
                    NextLevel();
                }    
            }
        }
    }



    public void UpdateCount()
    {
        currentCount--;
        gameManager.UpdateCountUI();
    }

    public void NextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set in the LevelManager script!");
        }
    }
}
