using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMarket : MonoBehaviour
{
    public GameObject popUpConfirmItem;
    public Text UIPrice;
    Item item;

    Market market;

    private void Start()
    {
        market = FindObjectOfType<Market>();
        item = GetComponent<Item>();
        UpdateUIPrice();
    }

    public void UpdateUIPrice()
    {
        UIPrice.text = item.price.ToString();
    }

    public void BuyHammer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        int money = data.money;

        if (money >= item.price)
        {
            market.hammerPrice = item.price;
            market.hammerId = item.id;

            market.itemToBuy = 1;

            popUpConfirmItem.SetActive(true);
        }
        else
        {
            print("No hay guita");
        }
    }

    public void BuyMole()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        int money = data.money;

        if (money >= item.price)
        {
            market.molePrice = item.price;
            market.moleId = item.id;

            market.itemToBuy = 0;

            popUpConfirmItem.SetActive(true);
        }
        else
        {
            print("No hay guita");
        }
    }
}
