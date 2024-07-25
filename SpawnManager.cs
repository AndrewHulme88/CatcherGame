using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject bigItemPrefab;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 3f;
    [SerializeField] private float minBigSpawnInterval = 10f;
    [SerializeField] private float maxBigSpawnInterval = 60f;
    [SerializeField] private float spawnXRange = 4f;

    private float timer;
    private float bigTimer;
    private float currentSpawnInterval;
    private float currentBigSpawnInterval;

    void Start()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        timer = 0f;
        currentBigSpawnInterval = Random.Range(minBigSpawnInterval, maxBigSpawnInterval);
        bigTimer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        bigTimer += Time.deltaTime;

        if(timer >= currentSpawnInterval)
        {
            SpawnItem();
            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            timer = 0f;
        }

        if(bigTimer >= currentBigSpawnInterval)
        {
            SpawnBigItem();
            currentBigSpawnInterval = Random.Range(minBigSpawnInterval, maxBigSpawnInterval);
            bigTimer = 0f;
        }
    }

    void SpawnItem()
    {
        float spawnX = Random.Range(-spawnXRange, spawnXRange);
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0f);

        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        newItem.transform.SetParent(transform);
    }

    void SpawnBigItem()
    {
        float spawnX = Random.Range(-spawnXRange, spawnXRange);
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0f);

        GameObject newItem = Instantiate(bigItemPrefab, spawnPosition, Quaternion.identity);
        newItem.transform.SetParent(transform);
    }
}
