using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 60;
    [SerializeField] float buildDelay = .1f;

    void Start()
    {
        StartCoroutine(BuildTowerRoutine());
    }

    IEnumerator BuildTowerRoutine()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
        }
    }

    public bool CreateTower(Tower tower, Vector3 pos)
    {
        GoldChest chest = FindObjectOfType<GoldChest>();

        if (chest == null)
        {
            return false;
        }

        if (chest.CurrentGold >= cost)
        {
            Instantiate(tower.gameObject,pos, Quaternion.identity);
            chest.Withdraw(cost);
            return true;
        }

        return false;

    }
}
