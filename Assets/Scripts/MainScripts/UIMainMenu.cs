using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Text scoreText;
    public Text moneyText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        scoreText.text = "Score: " + data.score.ToString();
        moneyText.text = data.money.ToString();
    }
}
