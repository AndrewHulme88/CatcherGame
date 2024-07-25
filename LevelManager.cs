using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TMP_Text countDownText;
    public int currentCount = 20;

    private GameManager gameManager;

    private void Update()
    {
        if(currentCount <= 0)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.NextLevel();
        }
    }

    public void UpdateCount()
    {
        currentCount--;
        UpdateCountUI();
    }

    void UpdateCountUI()
    {
        if (countDownText != null)
        {
            countDownText.text = currentCount.ToString() + " Items Remaining";
        }
    }
}
