using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<string, bool> unlockedMoles = new Dictionary<string, bool>();
    [HideInInspector]
    public Dictionary<string, bool> unlockedHammers = new Dictionary<string, bool>();
    [HideInInspector]
    public Dictionary<string, bool> selectedMoles = new Dictionary<string, bool>();
    [HideInInspector]
    public string selectedHammerID;

    public GameObject[] moleListItems;
    public GameObject[] hammerListItems;

    void Start()
    {
        Inventory();
        InventoryMoles();
        InventoryHammers();
    }

    public void Inventory()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        unlockedMoles = data.unlockedMoles;
        unlockedHammers = data.unlockedHammers;
        selectedHammerID = data.selectedHammerID;
        selectedMoles = data.selectedMoles;
    }

    public void InventoryMoles()
    {
        foreach (GameObject go in moleListItems)
        {
            Item item = go.GetComponent<Item>();

            if (unlockedMoles.ContainsKey(item.id))
            {
                go.SetActive(true);
                Toggle btn = go.GetComponent<Toggle>();

                if (selectedMoles.ContainsKey(item.id))
                {
                    btn.SetIsOnWithoutNotify(true);
                }
                else
                {
                    btn.SetIsOnWithoutNotify(false);
                }
            }
            else
            {
                go.SetActive(false);
            }
        }
    }

    public void InventoryHammers()
    {
        foreach (GameObject go in hammerListItems)
        {
            Item item = go.GetComponent<Item>();

            if (unlockedHammers.ContainsKey(item.id))
            {
                go.SetActive(true);
                Toggle btn = go.GetComponent<Toggle>();

                if (selectedHammerID == item.id)
                {
                    btn.SetIsOnWithoutNotify(true);
                }
                else
                {
                    btn.SetIsOnWithoutNotify(false);
                }
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
