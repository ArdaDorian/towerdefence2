using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldChest : MonoBehaviour
{
    [SerializeField] int startingGold = 150;
    int currentGold;
    [SerializeField] TextMeshProUGUI gold_text;

    public int CurrentGold { get { return currentGold; } }

    void Start()
    {
        currentGold= startingGold;
        DisplayGold();
    }

    public void Deposit(int amount)
    {
        currentGold += Mathf.Abs(amount);
        DisplayGold();
    }

    public void Withdraw(int amount)
    {
        currentGold -= Mathf.Abs(amount);
        if (currentGold< 0)
        {
            currentGold = 0;
            RestartScene();
        }
        DisplayGold();
    }

    private static void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void DisplayGold()
    {
        gold_text.text = "Gold: "+ currentGold.ToString("000");
    }
}
