using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int currentCount = 20;

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
            gameManager.NextLevel();
        }
    }

    public void UpdateCount()
    {
        currentCount--;
        gameManager.UpdateCountUI();
    }
}
