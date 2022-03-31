using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    Market market;

    public Text text_actualMoney;
    public Text text_postPayMoney;
    public Text text_price;

    GameManager manager;

    int postPayMoney;
    int actualMoney;

    public void UpdatePopUp()
    {
        market = FindObjectOfType<Market>();
        manager = FindObjectOfType<GameManager>();

        PlayerData data = SaveSystem.LoadPlayer();

        actualMoney = data.money;

        text_actualMoney.text = actualMoney.ToString();

        if(market.itemToBuy == 0)
        {
            postPayMoney = actualMoney - market.molePrice;
            text_price.text = "-" + market.molePrice.ToString();
        }
        else
        {
            postPayMoney = actualMoney - market.hammerPrice;
            text_price.text = "-" + market.hammerPrice.ToString();
        }

        text_postPayMoney.text = postPayMoney.ToString();
    }

    public void PurchaseItem()
    {
        if(market.itemToBuy == 0)
        {
            manager.unlockedMoles.Add(market.moleId, true);
        }
        else if(market.itemToBuy == 1)
        {
            manager.unlockedHammers.Add(market.hammerId, true);
        }

        manager.money = postPayMoney;

        SaveSystem.SavePlayer(manager);

        market.LoadMarketInfo();
    }
}
