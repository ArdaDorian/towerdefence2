using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(5f, 30f)] int poolSize = 5;
    GameObject[] pool;

    [SerializeField] [Range(1f, 30f)] float spawnTime;
    [SerializeField] GameObject enemyPrefab;

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    void PopulatePool()
    {
        pool= new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            InstantiateEnemyInPool();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void InstantiateEnemyInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
