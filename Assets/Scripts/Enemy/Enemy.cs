using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldDrop = 25;
    [SerializeField] int goldSteal = 15;

    GoldChest chest;

    void Awake()
    {
        chest=FindObjectOfType<GoldChest>();
    }

    public void DropGold()
    {
        chest.Deposit(goldDrop);
    }

    public void StealGold()
    {
        chest.Withdraw(goldSteal);
    }

} 