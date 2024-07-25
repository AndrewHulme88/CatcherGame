using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigItemPickup : MonoBehaviour
{
    [SerializeField] private int points = 10;
    private GameManager gameManager;
    private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager != null)
            {
                gameManager.UpdateScore(points);
            }

            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
