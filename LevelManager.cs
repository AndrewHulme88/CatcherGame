using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentCount = 20;
    public string nextSceneName;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        gameManager.remainingCount = currentCount;
        gameManager.InitializeCountUI();
    }

    private void Update()
    {
        if(currentCount <= 0)
        {
            NextLevel();
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
