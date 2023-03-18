using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;

    [Tooltip("Increase Max Hit Points")]
    [SerializeField] int difficultyIncreaser = 1;

    Enemy enemy;

    void Start()
    {
        enemy= GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            currentHitPoints = 0;
            maxHitPoints += difficultyIncreaser;
            ApplyDeath();
        }
    }

    void ApplyDeath()
    {
        gameObject.SetActive(false);
        enemy.DropGold();
    }
}
