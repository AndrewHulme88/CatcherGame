using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnXRange = 4f;

    private float timer;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    void SpawnItem()
    {
        float spawnX = Random.Range(-spawnXRange, spawnXRange);
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0f);


        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        newItem.transform.SetParent(transform);
    }
}
