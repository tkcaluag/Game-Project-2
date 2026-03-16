using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalPrefab;
    [SerializeField] private GameObject smallPrefab;

    [SerializeField] private float normalInterval = 5f;
    [SerializeField] private float smallInterval = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnEnemy(normalInterval, normalPrefab));
        StartCoroutine(spawnEnemy(smallInterval, smallPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-5f, 5), UnityEngine.Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
