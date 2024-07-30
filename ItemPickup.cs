using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private int points = 1;
    [SerializeField] private GameObject itemParticles;
    private GameManager gameManager;
    private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if(gameManager != null )
            {
                gameManager.UpdateScore(points);
            }

            ItemDestroy();
        }
        else
        {
            ItemDestroy();
        }

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.UpdateCount();
    }

    private void ItemDestroy()
    {
        Instantiate(itemParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
