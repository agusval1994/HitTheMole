using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public GameObject[] molesCatalog;
    public GameObject[] hammersCatalog;

    public Text text_money;

    int money;

    public Dictionary<string, bool> unlockedMoles = new Dictionary<string, bool>();
    public Dictionary<string, bool> unlockedHammers = new Dictionary<string, bool>();

    [HideInInspector]
    public int molePrice, hammerPrice;
    [HideInInspector]
    public string moleId, hammerId;

    public int itemToBuy = 0;  //0 = mole, 1 = hammer

    void Start()
    {
        LoadMarketInfo();
    }

    public void Catalog()
    {
        foreach (GameObject go in molesCatalog)
        {
            Item item = go.GetComponent<Item>();

            if (unlockedMoles.ContainsKey(item.id))
            {
                Button button = go.GetComponent<Button>();
                button.interactable = false;
            }
        }

        foreach (GameObject go in hammersCatalog)
        {
            Item item = go.GetComponent<Item>();

            if (unlockedHammers.ContainsKey(item.id))
            {
                Button button = go.GetComponent<Button>();
                button.interactable = false;
            }
        }
    }

    public void LoadMarketInfo()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        money = data.money;

        unlockedMoles = data.unlockedMoles;
        unlockedHammers = data.unlockedHammers;

        UpdateUI();
    }

    public void UpdateUI()
    {
        text_money.text = money.ToString();

        Catalog();
    }
}
